using Database.Users;

namespace DesktopClient.Authentication
{
    public static class CurrentAccount
    {
        public static User CurrentUser { get; set; }
    }
}