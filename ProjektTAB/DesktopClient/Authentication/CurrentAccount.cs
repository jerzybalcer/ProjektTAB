using Database.Users;
using Database.Users.Simplified;

namespace DesktopClient.Authentication
{
    public static class CurrentAccount
    {
        public static UserSimplified? CurrentUser { get; private set; }

        // temporary property for testing purposes
        public static bool IsLoggedIn { get; private set; } = false;

        public static void Logout()
        {
            CurrentUser = null;
            IsLoggedIn = false;
            MainWindow mainWindow = (MainWindow)App.Current.MainWindow;
            mainWindow.UserLoggedIn.Content = "Nie zalogowano";
            mainWindow.UserLoggedIn.IsEnabled = false;
            mainWindow.HideMenuButtons();
        }

        public static void Login(UserSimplified user)
        {
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
        }
    }
}