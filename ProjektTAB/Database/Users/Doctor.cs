using System.ComponentModel.DataAnnotations;

namespace Database.Users
{
    public class Doctor : User
    {
        [MaxLength(7)]
        public int LicenseNumber { get; set; }

        public Doctor(string name, string surname, UserAccount userAccount, int licenseNumber) : base(name, surname, userAccount)
        {
            LicenseNumber = licenseNumber;
        }
        private Doctor() : base()
        {

        }
    }
}
