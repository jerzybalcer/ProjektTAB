namespace Database.Users
{
    public class UserAccount
    {
        public int UserAccountId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public User User { get; set; }
        public bool IsActive { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiries { get; set; }

        public UserAccount(string email, string password)
        {
            Email = email;
            Password = password;
        }
        private UserAccount()
        {

        }
    }
}
