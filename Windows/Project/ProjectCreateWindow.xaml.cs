using System.Windows;
using StudyTracker.Model;
using StudyTracker.id;

namespace StudyTracker.Windows.Project
{
    /// <summary>
    /// Логика взаимодействия для ProjectCreateWindow.xaml
    /// </summary>
    public partial class ProjectCreateWindow : Window
    {
        public ProjectCreateWindow()
        {
            InitializeComponent();
        }

        private void AddProjectButton(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameBlock.Text))
            {
                MessageBox.Show("Введите название проекта!");
                return;
            }

            DataWorker.CreateProject(NameBlock.Text, UserId.Id);
            var wnd = new Windows.Main.MainAppWindow();
            wnd.Show();
            Close();
        }
    }
}
