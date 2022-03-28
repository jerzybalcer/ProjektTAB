namespace Database.People
{
    public class LabAssistant : User
    {
        public LabAssistant(string name, string surname, UserAccount userAccount) : base(name, surname, userAccount)
        {
        }
        private LabAssistant() : base()
        {

        }
    }
}
