using System.Collections.Generic;
using System.Windows;
using StudyTracker.Model;
using StudyTracker.id;

namespace StudyTracker.Windows.Main
{
    /// <summary>
    /// Interaction logic for MainAppWindow.xaml
    /// </summary>
    public partial class MainAppWindow : Window
    {
        public List<Project> ProjectsList { get; } = DataWorker.GetAllProjects(UserId.Id);

        public MainAppWindow()
        {
            InitializeComponent();
            RefreshData();
        }

        public void RefreshData()
        {
            var projects = DataWorker.GetAllProjects(UserId.Id);
            Projects.ItemsSource = projects;
            MyTaskProjects.ItemsSource = projects;
            UsersProject.ItemsSource = projects;

            if (projects.Count > 0)
            {
                var tasks = DataWorker.GetTasks(projects[0].Id);
                Tasks.ItemsSource = tasks;
                var myTasks = DataWorker.GetMyTasks(projects[0].Id, UserId.Id);
                MyTask.ItemsSource = myTasks;
                var users = DataWorker.GetProjectUsers(projects[0].Id);
                Users.ItemsSource = users;
            }
            else
            {
                ProjectId.Id = 0;
            }

            Projects.SelectedIndex = 0;
            MyTaskProjects.SelectedIndex = 0;
            MyTask.SelectedIndex = 0;
            UsersProject.SelectedIndex = 0;
        }

        private void CreateProject(object sender, RoutedEventArgs e)
        {
            var wnd = new AddProjectWindow();
            wnd.Show();
            Close();
        }

        private void CreateTask(object sender, RoutedEventArgs e)
        {
            if (ProjectId.Id == 0)
            {
                MessageBox.Show("Вы не выбрали проект!");
                return;
            }

            if (DataWorker.CheckAccess(ProjectId.Id, UserId.Id))
            {
                var wnd = new AddTaskWindow();
                wnd.Show();
                Close();
            }
            else
            {
                MessageBox.Show("У вас нету доступа!");
            }
        }

        private void AddUserButton(object sender, RoutedEventArgs e)
        {
            if (ProjectId.Id == 0)
            {
                MessageBox.Show("Вы не выбрали проект");
                return;
            }

            if (DataWorker.CheckAccess(ProjectId.Id, UserId.Id))
            {
                var wnd = new AddProjectUserWindow();
                wnd.Show();
                Close();
            }
            else
            {
                MessageBox.Show("У вас нету доступа!");
            }
        }

        private void Projects_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (Projects.SelectedItem is Project project)
            {
                ProjectId.Id = project.Id;
                var tasks = DataWorker.GetTasks(ProjectId.Id);
                Tasks.ItemsSource = tasks;
            }
        }

        private void DeleteTaskButton(object sender, RoutedEventArgs e)
        {
            if (Tasks.SelectedItem is not Task task)
            {
                MessageBox.Show("Вы не выбрали задачу!");
                return;
            }

            if (DataWorker.CheckAccess(ProjectId.Id, UserId.Id))
            {
                DataWorker.DeleteTask(ProjectId.Id, task.Id);
                RefreshData();
            }
            else
            {
                MessageBox.Show("У вас нету доступа!");
            }
        }

        private void DeleteProjectButton(object sender, RoutedEventArgs e)
        {
            if (ProjectId.Id == 0)
            {
                MessageBox.Show("Вы не выбрали проект!");
                return;
            }

            DataWorker.DeleteProject(ProjectId.Id, UserId.Id);
            RefreshData();
        }

        private void DeleteUserButton(object sender, RoutedEventArgs e)
        {
            if (DataWorker.CheckAccess(ProjectId.Id, UserId.Id))
            {
                var wnd = new DeleteUser();
                wnd.Show();
                Close();
            }
            else
            {
                MessageBox.Show("У вас нету доступа!");
            }
        }

        private void MyTaskProjects_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (MyTaskProjects.SelectedItem is Project project)
            {
                ProjectId.Id = project.Id;
                var tasks = DataWorker.GetMyTasks(ProjectId.Id, UserId.Id);
                MyTask.ItemsSource = tasks;
            }
        }

        private void UsersProject_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (UsersProject.SelectedItem is Project project)
            {
                ProjectId.Id = project.Id;
                var users = DataWorker.GetProjectUsers(ProjectId.Id);
                Users.ItemsSource = users;
            }
        }

        private void RefreshButton(object sender, RoutedEventArgs e)
        {
            RefreshData();
        }

        private void DoTaskButton(object sender, RoutedEventArgs e)
        {
            if (Tasks.SelectedItem is not Task task)
            {
                MessageBox.Show("Вы не выбрали задачу!");
                return;
            }

            if (DataWorker.ChangeStatus(ProjectId.Id, task.Id, UserId.Id))
            {
                RefreshData();
            }
            else
            {
                MessageBox.Show("Вы можете выполнять только свои задачи!");
            }
        }

        private void AddRole(object sender, RoutedEventArgs e)
        {
            if (DataWorker.CheckAccess(ProjectId.Id, UserId.Id))
            {
                var wnd = new AddRoleWindow();
                wnd.Show();
                Close();
            }
            else
            {
                MessageBox.Show("У вас нету доступа!");
            }
        }

        private void SetRoleButton(object sender, RoutedEventArgs e)
        {
            if (DataWorker.CheckAccess(ProjectId.Id, UserId.Id))
            {
                var wnd = new SetRoleWindow();
                wnd.Show();
                Close();
            }
            else
            {
                MessageBox.Show("У вас нету доступа!");
            }
        }
    }
}
