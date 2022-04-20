using Database;
using Database.Users.Simplified;
using DesktopClient.Authentication;
using DesktopClient.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

namespace DesktopClient.Pages.SharedPages
{
    /// <summary>
    /// Interaction logic for MyAccountPage.xaml
    /// </summary>
    public partial class MyAccountPage : Page
    {
        public MyAccountPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            FirstName.Text = CurrentAccount.CurrentUser.Name;
            LastName.Text = CurrentAccount.CurrentUser.Surname;
            Email.Text = CurrentAccount.CurrentUser.Email;
        }

        private async void SaveAccountDetailsBtn_Click(object sender, RoutedEventArgs e)
        {
            if(FirstName.Text.TrimEnd().Length == 0 || LastName.Text.TrimEnd().Length == 0 || Email.Text.TrimEnd().Length == 0)
            {
                MessageBox.Show("Uzupełnij wszystkie dane!");
                return;
            }

            var modifiedWorker = new UserSimplified 
            {
                UserId = CurrentAccount.CurrentUser.UserId,
                Name = FirstName.Text,
                Surname = LastName.Text,
                Email = Email.Text,
            };

    
            var tokenResponse = await ApiCaller.Post("api/Users/ChangeUserDetails", modifiedWorker);

            if (tokenResponse.IsSuccessStatusCode)
            {
                MessageBox.Show("Poprawnie zmieniono dane konta");

                CurrentAccount.CurrentUser.Name = modifiedWorker.Name;
                CurrentAccount.CurrentUser.Surname = modifiedWorker.Surname;
                CurrentAccount.CurrentUser.Email = modifiedWorker.Email;
            }
            else
            {
                MessageBox.Show("Błąd podczas zmiany danych! Skontaktuj się z administratorem");
            }
        }

        private async void ChangePasswordBtn_Click(object sender, RoutedEventArgs e)
        {
            if (OldPassword.SecurePassword.Length == 0 || NewPassword.SecurePassword.Length == 0 || RepeatNewPassword.SecurePassword.Length == 0)
            {
                MessageBox.Show("Uzupełnij wszystkie dane!");
                return;
            }

            if(NewPassword.Password != RepeatNewPassword.Password)
            {
                MessageBox.Show("Podane hasła muszą być jednakowe!");
                return;
            }

            SHA512 sha512Hash = SHA512.Create();
            byte[] oldPassSourceBytes = Encoding.UTF8.GetBytes(OldPassword.Password);
            byte[] oldPassHashBytes = sha512Hash.ComputeHash(oldPassSourceBytes);
            string oldHashPassword = BitConverter.ToString(oldPassHashBytes).Replace("-", String.Empty);

            byte[] newPassSourceBytes = Encoding.UTF8.GetBytes(NewPassword.Password);
            byte[] newPassHashBytes = sha512Hash.ComputeHash(newPassSourceBytes);
            string newHashPassword = BitConverter.ToString(newPassHashBytes).Replace("-", String.Empty);

            var tokenResponse = await ApiCaller.Post("ChangePassword", new PasswordChangeRequest(oldHashPassword, newHashPassword));

            if (tokenResponse.IsSuccessStatusCode)
            {
                MessageBox.Show("Poprawnie zmieniono hasło");
            }
            else
            {
                MessageBox.Show("Nieudana próba zmiany hasła! Upewnij się, że wpisujesz poprawne hasło.");
            }

        }
    }
}
