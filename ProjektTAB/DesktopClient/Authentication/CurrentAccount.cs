using Database;
using Database.Users.Simplified;
using DesktopClient.Helpers;
using DesktopClient.Pages.SharedPages;
using Newtonsoft.Json;
using System;
using System.Windows;
using System.Windows.Threading;

namespace DesktopClient.Authentication
{
    public static class CurrentAccount
    {
        public static UserSimplified? CurrentUser { get; private set; }

        // temporary property for testing purposes
        public static bool IsLoggedIn { get; private set; } = false;
        public static TokensPair TokensPair { get; set; } = new TokensPair("", "");

        private static readonly DispatcherTimer _refreshTimer = new DispatcherTimer();

        public static void Logout()
        {
            TokensPair.RefreshToken = "";
            TokensPair.Token = "";
            CurrentUser = null;
            IsLoggedIn = false;
            MainWindow mainWindow = (MainWindow)App.Current.MainWindow;
            mainWindow.UserLoggedIn.Content = "Nie zalogowano";
            mainWindow.UserLoggedIn.IsEnabled = false;
            mainWindow.HideMenuButtons();

            mainWindow.ContentFrame.Navigate(new LoginPage());
        }

        public static void Login(UserSimplified user)
        {
            _refreshTimer.Interval = TimeSpan.FromMinutes(14);
            _refreshTimer.Tick += TryRefreshToken;
            _refreshTimer.Start();

            CurrentUser = user;
            IsLoggedIn = true;
            MainWindow mainWindow = (MainWindow)App.Current.MainWindow;
            mainWindow.UserLoggedIn.Content = user.Name + " " + user.Surname;
            mainWindow.UserLoggedIn.IsEnabled = true;

            if (user.Role == Role.Receptionist)
            {
                mainWindow.ShowMenuButtons("Receptionist");
            }
            else if (user.Role == Role.Doctor)
            {
                mainWindow.ShowMenuButtons("Doctor");
            }
            else if (user.Role == Role.LabAssistant)
            {
                mainWindow.ShowMenuButtons("LabAssistant");
            }
            else if (user.Role == Role.LabManager)
            {
                mainWindow.ShowMenuButtons("LabManager");
            }
            else if (user.Role == Role.Admin)
            {
                mainWindow.ShowMenuButtons("Admin");
            }
        }

        private async static void TryRefreshToken(object? sender, EventArgs e)
        {
            var newTokensResponse = await ApiCaller.Post("RefreshToken", TokensPair);

            if (newTokensResponse.IsSuccessStatusCode)
            {
                var newTokensString = await newTokensResponse.Content.ReadAsStringAsync();
                TokensPair = JsonConvert.DeserializeObject<TokensPair>(newTokensString);
            }
            else
            {
                MessageBox.Show("Nie udało się odnowić sesji. Zaloguj się ponownie.");
                Logout();
            }
        }
    }
}