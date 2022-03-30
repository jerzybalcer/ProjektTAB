using System.ComponentModel.DataAnnotations;

namespace Database.People
{
    public class Patient
    {
        public int PatientId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        [MaxLength(11)]
        public string Pesel { get; set; }
        public Address Address { get; set; }


        public Patient(string name, string surname, string pesel, Address address)
        {
            Name = name;
            Surname = surname;
            Address = address;
            Pesel = pesel;
        }

        private Patient()
        {

        }
    }
}
