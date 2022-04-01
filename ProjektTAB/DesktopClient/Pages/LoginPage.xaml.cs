using DesktopClient.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesktopClient.Pages
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            CurrentAccount.IsLoggedIn = true;

            // retrieve data from the form
            string email = Email.Text;
            SecureString password = Password.SecurePassword;
            // call authentication api

            MainWindow mainWindow = (MainWindow)App.Current.MainWindow;
            mainWindow.ChangeMenuButtonVisibility(Visibility.Visible);

            // uncomment after api call implementation
            // mainWindow.UserLoggedInText.Text = LoggedUser.Name + " " + LoggedUser.Surname;

            // pass logged user object into page ctor
            this.NavigationService.Navigate(new LoggedAsPage());
        }
    }
}
