using System.ComponentModel.DataAnnotations;

namespace Database.Users
{
    public class Doctor : User
    {
        [MaxLength(7)]
        public string LicenseNumber { get; set; }

        public Doctor(string name, string surname, UserAccount userAccount, string licenseNumber) : base(name, surname, userAccount)
        {
            LicenseNumber = licenseNumber;
        }
        private Doctor() : base()
        {

        }
    }
}
