using Database.Users;
using DesktopClient.Authentication;
using System.Windows;
using System.Windows.Controls;

namespace DesktopClient.Pages.SharedPages
{
    /// <summary>
    /// Interaction logic for LoggedAsPage.xaml
    /// </summary>
    public partial class LoggedAsPage : Page
    {
        private readonly User? _loggedUser = null;

        public LoggedAsPage()
        {
            InitializeComponent();
        }

        public LoggedAsPage(User loggedUser)
        {
            InitializeComponent();
            _loggedUser = loggedUser;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (_loggedUser is not null)
            {
                LoggedUserName.Text = _loggedUser.Name + " " + _loggedUser.Surname;
            }
            else
            {
                LoggedUserName.Text = "nie zalogowano";
            }
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentAccount.Logout();

            NavigationService.Navigate(new LoginPage());
        }
    }
}
