using Database;
using Database.Users;
using Database.Users.Simplified;
using DesktopClient.Authentication;
using DesktopClient.Helpers;
using Newtonsoft.Json;
using System.Security;
using System.Web;
using System.Windows;
using System.Windows.Controls;

namespace DesktopClient.Pages.SharedPages
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

        private async void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            WaitingText.Visibility = Visibility.Visible;

            // retrieve data from the form
            string email = Email.Text;
            email = email.Replace("@", "%40");
            SecureString securePassword = Password.SecurePassword;
            string password = new System.Net.NetworkCredential(string.Empty, securePassword).Password;
            string encryptedPassword = password.Encrypt();
            encryptedPassword = HttpUtility.HtmlEncode(encryptedPassword);
            encryptedPassword = encryptedPassword.Replace("/", "%2F");
            // call authentication api to get token
            var tokenResponse = await ApiCaller.Post("Login", new UserLogin(email, encryptedPassword));

            if (tokenResponse.IsSuccessStatusCode)
            {
                var tokenStringRes = await tokenResponse.Content.ReadAsStringAsync();
                var tokenObject = JsonConvert.DeserializeObject<TokensPair>(tokenStringRes);

                CurrentAccount.TokensPair = tokenObject;

                // use token to get logged user
                var loginResponse = await ApiCaller.Get("GetUser/" + email + "/" + encryptedPassword);

                if (loginResponse.IsSuccessStatusCode)
                {
                    var loginResponseString = await loginResponse.Content.ReadAsStringAsync();
                    var jsonSettings = JsonConfiguration.GetJsonSettings();
                    UserSimplified? responseUser = JsonConvert.DeserializeObject<UserSimplified>(loginResponseString, jsonSettings);

                    if (responseUser is not null)
                    {
                        CurrentAccount.Login(responseUser);
                        NavigationService.Navigate(new LoggedAsPage(responseUser));
                    }
                    else
                    {
                        MessageBox.Show("Błąd podczas pobierania użytkownika z serwera!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Nieprawidłowe dane logowania!");
            }

            WaitingText.Visibility = Visibility.Hidden;
        }
    }
}
