namespace Database.People
{
    public class Doctor : User
    {
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
