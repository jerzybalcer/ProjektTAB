namespace Database.People
{
    public class Patient : Person
    {
        public Address Address { get; set; }
        public string Pesel { get; set; }

        public Patient(string name, string surname, Address address, string pesel) : base(name, surname)
        {
            Address = address;
            Pesel = pesel;
        }
    }
}
