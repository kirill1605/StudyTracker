using StudyTracker.id;
using StudyTracker.Model.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StudyTracker.Model
{
    public class DataWorker
    {
        // User's query
        public static bool CreateUser(string name, string password)
        {
            using var db = new Context();
            if (db.Users.Any(user => user.Name == name))
                return false;
            var user = new User { Name = name, Password = password };
            db.Users.Add(user);
            db.SaveChanges();
            return true;
        }

        public static int LoginUser(string name, string password)
        {
            using var db = new Context();
            var user = db.Users.FirstOrDefault(u => u.Name == name && u.Password == password);
            return user?.Id ?? 0;
        }

        public static List<User> GetUsers()
        {
            using var db = new Context();
            return db.Users.ToList();
        }

        // Project's query
        public static string CreateProject(string name, int userId)
        {
            using var db = new Context();
            var user = db.Users.First(admin => admin.Id == userId);
            var project = new Project { Name = name, Admin = user };
            db.Projects.Add(project);
            db.SaveChanges();
            var projectUser = new ProjectUser { UserId = user, ProjectId = project, RoleId = null };
            db.ProjectUsers.Add(projectUser);
            db.SaveChanges();
            return string.Empty;
        }

        public static void DeleteProject(int projectId, int userId)
        {
            using var db = new Context();
            var project = db.Projects.Include(p => p.Admin).First(p => p.Id == projectId);
            if (userId == project.Admin.Id)
            {
                db.Projects.Remove(project);
                db.SaveChanges();
            }
            else
            {
                var user = db.ProjectUsers.First(u => u.ProjectId == project && u.UserId.Id == userId);
                db.ProjectUsers.Remove(user);
                db.SaveChanges();
            }
        }

        public static List<Project> GetAllProjects(int userId)
        {
            using var db = new Context();
            var projectUsers = db.ProjectUsers.Include(p => p.UserId).Include(p => p.ProjectId).Include(p => p.ProjectId.Admin).Where(p => p.UserId.Id == userId).ToList();
            var userProjects = new List<Project>();
            foreach (var project in projectUsers)
                userProjects.Add(project.ProjectId);
            return userProjects;
        }

        // Role's query
        public static string AddRole(string name, int projectId)
        {
            using var db = new Context();
            var project = db.Projects.First(p => p.Id == projectId);
            var role = new Role { Name = name, ProjectId = project };
            db.Roles.Add(role);
            db.SaveChanges();
            return "Роль создана";
        }

        public static void SetRole(int userId, int projectId, int roleId)
        {
            using var db = new Context();
            var user = db.ProjectUsers.First(u => u.Id == userId && u.ProjectId.Id == projectId);
            var role = db.Roles.First(r => r.Id == roleId);
            user.RoleId = role;
            db.SaveChanges();
        }

        public static List<Role> GetRoles(int projectId)
        {
            using var db = new Context();
            return db.Roles.Where(r => r.ProjectId.Id == projectId).ToList();
        }

        // ProjectUser's query
        public static string AddUserToProject(int projectId, int userId, int? roleId)
        {
            using var db = new Context();
            var user = db.Users.First(u => u.Id == userId);
            var project = db.Projects.First(p => p.Id == projectId);
            var projectUser = new ProjectUser
            {
                ProjectId = project,
                UserId = user,
                RoleId = roleId != null ? db.Roles.First(r => r.Id == roleId) : null
            };
            db.ProjectUsers.Add(projectUser);
            db.SaveChanges();
            return "Пользователь добавлен";
        }

        public static bool DeleteProjectUser(int projectUserId, int projectId)
        {
            using var db = new Context();
            var user = db.ProjectUsers.Include(u => u.UserId).First(u => u.ProjectId.Id == projectId && u.Id == projectUserId);
            var admin = db.Projects.FirstOrDefault(p => p.Admin.Id == user.UserId.Id && p.Id == projectId);
            if (admin != null)
            {
                if (admin.Admin.Id != user.UserId.Id)
                {
                    db.ProjectUsers.Remove(user);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            db.ProjectUsers.Remove(user);
            db.SaveChanges();
            return true;
        }

        public static List<ProjectUser> GetProjectUsers(int projectId)
        {
            using var db = new Context();
            return db.ProjectUsers.Include(u => u.ProjectId).Include(u => u.UserId).Include(u => u.RoleId).Where(u => u.ProjectId.Id == projectId).ToList();
        }

        // Task's query
        public static string AddTask(string description, DateTime deadline, int projectId, int projectUserId)
        {
            using var db = new Context();
            var project = db.Projects.First(p => p.Id == projectId);
            var user = db.ProjectUsers.First(u => u.Id == projectUserId && u.ProjectId == project);
            var task = new Task
            {
                Description = description,
                CreatedTime = DateTime.UtcNow,
                Deadline = deadline,
                ProjectId = project,
                ProjectUserId = user,
                IsCompleted = false
            };
            db.Tasks.Add(task);
            db.SaveChanges();
            return string.Empty;
        }

        public static bool DeleteTask(int projectId, int? taskId)
        {
            if (taskId == null) return false;
            using var db = new Context();
            var task = db.Tasks.First(p => p.Id == taskId && p.ProjectId.Id == projectId);
            db.Tasks.Remove(task);
            db.SaveChanges();
            return true;
        }

        public static List<Task> GetTasks(int projectId)
        {
            using var db = new Context();
            return db.Tasks.Include(t => t.ProjectId).Include(t => t.ProjectUserId).Include(t => t.ProjectUserId.UserId).Where(t => t.ProjectId.Id == projectId).ToList();
        }

        public static List<Task> GetMyTasks(int projectId, int userId)
        {
            using var db = new Context();
            return db.Tasks.Include(t => t.ProjectId).Include(t => t.ProjectUserId).Include(t => t.ProjectUserId.UserId).Where(t => t.ProjectId.Id == projectId && t.ProjectUserId.UserId.Id == userId).ToList();
        }

        public static bool ChangeStatus(int projectId, int taskId, int userId)
        {
            using var db = new Context();
            var task = db.Tasks.FirstOrDefault(p => p.Id == taskId && p.ProjectId.Id == projectId && p.ProjectUserId.UserId.Id == userId);
            if (task == null)
                return false;
            task.IsCompleted = true;
            db.SaveChanges();
            return true;
        }

        public static bool CheckAccess(int projectId, int userId)
        {
            using var db = new Context();
            var project = db.Projects.FirstOrDefault(p => p.Admin.Id == userId);
            return project != null;
        }
    }
}
