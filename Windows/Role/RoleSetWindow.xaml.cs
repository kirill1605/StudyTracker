using StudyTracker.id;
using StudyTracker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace StudyTracker.Windows.Role
{
    /// <summary>
    /// Логика взаимодействия для RoleSetWindow.xaml
    /// </summary>
    public partial class RoleSetWindow : Window
    {
        private readonly List<ProjectUser> _users = DataWorker.GetProjectUsers(ProjectId.Id);
        private readonly List<Role> _roles = DataWorker.GetRoles(ProjectId.Id);

        public RoleSetWindow()
        {
            InitializeComponent();
            var names = new List<string>();
            foreach (var user in _users)
            {
                names.Add(user.UserId.Name);
            }
            User.ItemsSource = names;

            var rolesNames = new List<string>();
            foreach (var role in _roles)
            {
                rolesNames.Add(role.Name);
            }
            Role.ItemsSource = rolesNames;
        }

        private void SetRoleButton(object sender, RoutedEventArgs e)
        {
            if (User.SelectedItem == null || Role.SelectedItem == null)
            {
                MessageBox.Show("Ошибка валидации!");
                return;
            }

            int userId = _users.First(user => user.UserId.Name == User.SelectedItem).Id;
            int roleId = _roles.First(role => role.Name == Role.SelectedItem).Id;
            DataWorker.SetRole(userId, ProjectId.Id, roleId);
            var wnd = new Windows.Main.MainAppWindow();
            wnd.Show();
            Close();
        }
    }
}
