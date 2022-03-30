namespace Database.Users
{
    public abstract class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public UserAccount UserAccount { get; set; }
        public int UserAccountId { get; set; }

        public User(string name, string surname, UserAccount userAccount)
        {
            Name = name;
            Surname = surname;
            UserAccount = userAccount;
            UserAccountId = UserAccount.UserAccountId;
        }

        protected User()
        {
        }
    }
}
