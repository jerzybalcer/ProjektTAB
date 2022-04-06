using Database;
using Database.Users;
using DesktopClient.Authentication;
using DesktopClient.Helpers;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security;
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
            // retrieve data from the form
            string email = Email.Text;
            email = email.Replace("@", "%40");
            SecureString securePassword = Password.SecurePassword;
            string password = new System.Net.NetworkCredential(string.Empty, securePassword).Password;

            // call authentication api
            HttpResponseMessage response = await ApiCaller.Get("Login/" + email + "/" + password);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var jsonSettings = JsonConfiguration.GetJsonSettings();
                User? responseObject = JsonConvert.DeserializeObject<User>(responseString, jsonSettings);

                if (responseObject is not null)
                {
                    CurrentAccount.Login(responseObject);
                    NavigationService.Navigate(new LoggedAsPage(responseObject));
                }
                else
                {
                    MessageBox.Show("Błąd podczas pobierania użytkownika z serwera!");
                }
            }
            else
            {
                MessageBox.Show("Nieprawidłowe dane logowania!");
            }
        }
    }
}
