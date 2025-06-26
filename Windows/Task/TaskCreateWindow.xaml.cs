using StudyTracker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using StudyTracker.id;

namespace StudyTracker.Windows.Task
{
    /// <summary>
    /// Логика взаимодействия для TaskCreateWindow.xaml
    /// </summary>
    public partial class TaskCreateWindow : Window
    {
        private readonly List<ProjectUser> _users = DataWorker.GetProjectUsers(ProjectId.Id);

        public TaskCreateWindow()
        {
            InitializeComponent();
            var names = new List<string>();
            foreach (var user in _users)
            {
                names.Add(user.UserId.Name);
            }
            User.ItemsSource = names;
        }

        private void CreateTask(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Description.Text) || !Deadline.SelectedDate.HasValue || User.SelectedItem == null)
            {
                MessageBox.Show("Ошибка валидации!");
                return;
            }

            int userId = _users.First(user => user.UserId.Name == User.SelectedItem).Id;
            DateTime deadline = Deadline.SelectedDate.Value;
            DataWorker.AddTask(Description.Text, deadline, ProjectId.Id, userId);
            var wnd = new Windows.Main.MainAppWindow();
            wnd.Show();
            Close();
        }
    }
}
