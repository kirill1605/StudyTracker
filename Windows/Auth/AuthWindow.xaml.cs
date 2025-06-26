using StudyTracker.Model;
using StudyTracker.id;
using System.Windows;

namespace StudyTracker.Windows.Auth
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    /// 
    

    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
        }

        private bool CheckValidation(string name, string password)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(password))
            {
                return false;
            }
            return true;
        }

        private void LoginButton(object sender, RoutedEventArgs e)
        {
            if (CheckValidation(NameBlock.Text, PasswordBlock.Password))
            {
                int id = DataWorker.LoginUser(NameBlock.Text, PasswordBlock.Password);
                if (id == 0)
                {
                    MessageBox.Show("Неверный пароль!");
                }
                else
                {
                    UserId.Id = id;
                    var main = new Windows.Main.MainAppWindow();
                    main.Show();
                    Close();
                }
            }
            else
            {
                MessageBox.Show("Ошибка валидации!");
            }
        }

        private void RegistrationButton(object sender, RoutedEventArgs e)
        {
            if (CheckValidation(NameBlock.Text, PasswordBlock.Password))
            {
                if (DataWorker.CreateUser(NameBlock.Text, PasswordBlock.Password))
                {
                    LoginButton(sender, e);
                }
                else
                {
                    MessageBox.Show("Пользователь с таким именем уже существует");
                }
            }
            else
            {
                MessageBox.Show("Ошибка валидации!");
            }
        }
    }
}
