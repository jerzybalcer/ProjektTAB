namespace Database.People
{
    public abstract class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public User(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }

        protected User()
        {
        }
    }
}
