using Database.Users;
using DesktopClient.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Security;
using Newtonsoft.Json;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

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

        private async void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            CurrentAccount.IsLoggedIn = true;

            // retrieve data from the form
            string email = Email.Text;
            email = email.Replace("@","%40");
            SecureString securePassword = Password.SecurePassword;
            string password = new System.Net.NetworkCredential(string.Empty, securePassword).Password;
            // call authentication api
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7062");
            HttpResponseMessage response = await client.GetAsync("Login/"+email+"/"+password);
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var responseObject = JsonConvert.DeserializeObject<Receptionist>(responseString);
                MainWindow mainWindow = (MainWindow)App.Current.MainWindow;
                mainWindow.ChangeMenuButtonVisibility(Visibility.Visible);
                mainWindow.UserLoggedInText.Text = responseObject.Name + " " + responseObject.Surname;
                this.NavigationService.Navigate(new LoggedAsPage(responseObject));
            }
        }
    }
}
