using DesktopClient.Authentication;
using DesktopClient.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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

        private void SaveAccountDetailsBtn_Click(object sender, RoutedEventArgs e)
        {
            if(FirstName.Text.TrimEnd().Length == 0 || LastName.Text.TrimEnd().Length == 0 || Email.Text.TrimEnd().Length == 0)
            {
                MessageBox.Show("Uzupełnij wszystkie dane!");
                return;
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

            var tokenResponse = await ApiCaller.Post("ChangePassword", NewPassword.Password);

            if (tokenResponse.IsSuccessStatusCode)
            {
                MessageBox.Show("Poprawnie zmieniono hasło");
            }
            else
            {
                MessageBox.Show("Nieprawidłowe dane logowania!");
            }

        }
    }
}
