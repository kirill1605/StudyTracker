using StudyTracker.Model;
using System.Windows;
using StudyTracker.id;
using System.Collections.Generic;
using System.Linq;

namespace StudyTracker.Windows.User
{
    /// <summary>
    /// Логика взаимодействия для UserDeleteWindow.xaml
    /// </summary>
    public partial class UserDeleteWindow : Window
    {
        private readonly List<ProjectUser> _users = DataWorker.GetProjectUsers(ProjectId.Id);
        private readonly List<string> _names = new();

        public UserDeleteWindow()
        {
            InitializeComponent();
            foreach (var user in _users)
            {
                _names.Add(user.UserId.Name);
            }
            Users.ItemsSource = _names;
        }

        private void DeleteUserButton(object sender, RoutedEventArgs e)
        {
            if (Users.SelectedItem == null)
            {
                MessageBox.Show("Выберите пользователя!");
                return;
            }

            int userId = _users.First(u => u.UserId.Name == Users.SelectedValue).Id;
            if (DataWorker.DeleteProjectUser(userId, ProjectId.Id))
            {
                var wnd = new Windows.Main.MainAppWindow();
                wnd.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Пользователь не может быть удалён!");
            }
        }
    }
}
