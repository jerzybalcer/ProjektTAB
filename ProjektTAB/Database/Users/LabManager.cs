namespace Database.Users
{
    public class LabManager : User
    {
        public LabManager(string name, string surname, UserAccount userAccount) : base(name, surname, userAccount)
        {
        }

        private LabManager() : base()
        {

        }
    }
}
