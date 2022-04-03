using Database.Users;

namespace DesktopClient.Authentication
{
    public static class CurrentAccount
    {
        public static User? CurrentUser { get; private set; }

        // temporary property for testing purposes
        public static bool IsLoggedIn { get; private set; } = false;

        public static void Logout()
        {
            CurrentUser = null;
            IsLoggedIn = false;
            MainWindow mainWindow = (MainWindow)App.Current.MainWindow;
            mainWindow.UserLoggedIn.Content = "Nie zalogowano";
            mainWindow.UserLoggedIn.IsEnabled = false;
            mainWindow.ChangeMenuButtonVisibility(System.Windows.Visibility.Collapsed);
        }

        public static void Login(User user)
        {
            CurrentUser = user;
            IsLoggedIn = true;
            MainWindow mainWindow = (MainWindow)App.Current.MainWindow;
            mainWindow.UserLoggedIn.Content = user.Name + " " + user.Surname;
            mainWindow.UserLoggedIn.IsEnabled = true;
            mainWindow.ChangeMenuButtonVisibility(System.Windows.Visibility.Visible);
        }
    }
}