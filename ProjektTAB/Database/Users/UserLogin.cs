namespace Database.Users
{
    public class UserLogin
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public UserLogin(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
