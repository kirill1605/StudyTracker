using StudyTracker.id;
using StudyTracker.Model;
using System.Windows;

namespace StudyTracker.Windows.Role
{
    /// <summary>
    /// Логика взаимодействия для RoleCreateWindow.xaml
    /// </summary>
    public partial class RoleCreateWindow : Window
    {
        public RoleCreateWindow()
        {
            InitializeComponent();
        }

        private void AddRoleButton(object sender, RoutedEventArgs e)
        {
            DataWorker.AddRole(RoleBlock.Text, ProjectId.Id);
            var wnd = new Windows.Main.MainAppWindow();
            wnd.Show();
            Close();
        }
    }
}
