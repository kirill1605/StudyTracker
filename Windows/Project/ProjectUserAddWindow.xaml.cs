using StudyTracker.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using StudyTracker.id;

namespace StudyTracker.Windows.Project
{
    /// <summary>
    /// Логика взаимодействия для ProjectUserAddWindow.xaml
    /// </summary>
    public partial class ProjectUserAddWindow : Window
    {
        private readonly List<User> _users = DataWorker.GetUsers();
        private readonly List<string> _names = new();

        public ProjectUserAddWindow()
        {
            InitializeComponent();
            foreach (var user in _users)
            {
                _names.Add(user.Name);
            }
            Users.ItemsSource = _names;
        }

        private void AddNewUserButton(object sender, RoutedEventArgs e)
        {
            if (Users.SelectedItem == null)
            {
                MessageBox.Show("Выберите пользователя");
                return;
            }

            int userId = _users.First(u => u.Name == Users.SelectedValue).Id;
            if (DataWorker.GetProjectUsers(ProjectId.Id).Any(u => u.UserId.Id == userId))
            {
                MessageBox.Show("Пользователь уже есть в проекте!");
                return;
            }

            DataWorker.AddUserToProject(ProjectId.Id, userId, null);
            var wnd = new Windows.Main.MainAppWindow();
            wnd.Show();
            Close();
        }
    }
}
