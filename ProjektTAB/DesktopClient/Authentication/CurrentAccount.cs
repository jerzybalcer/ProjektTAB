using Database.Users;

namespace DesktopClient.Authentication
{
    public static class CurrentAccount
    {
        public static User CurrentUser { get; set; }

        // temporary property for testing purposes
        public static bool IsLoggedIn { get; set; } = false;
    }
}