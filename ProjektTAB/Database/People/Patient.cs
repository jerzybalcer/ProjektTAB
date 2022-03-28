namespace Database.People
{
    public class Patient : User
    {
        public Address Address { get; set; }
        public string Pesel { get; set; }

        public Patient(string name, string surname, UserAccount userAccount, Address address, string pesel) : base(name, surname, userAccount)
        {
            Address = address;
            Pesel = pesel;
        }

        private Patient() : base()
        {

        }
    }
}
