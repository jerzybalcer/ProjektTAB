namespace Database.Users
{
    public class Receptionist : User
    {
        public Receptionist(string name, string surname, UserAccount userAccount) : base(name, surname, userAccount)
        {
        }
        private Receptionist() : base()
        {

        }
    }
}
