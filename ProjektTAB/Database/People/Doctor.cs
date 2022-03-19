namespace Database.People
{
    public class Doctor : User
    {
        public int LicenseNumber { get; set; }

        public Doctor(string name, string surname, int licenseNumber) : base(name, surname)
        {
            LicenseNumber = licenseNumber;
        }
    }
}
