namespace Database.Users.Simplified
{
    public class UserSimplified
    {
        public UserSimplified()
        {
        }

        public UserSimplified(int userId, string name, string surname, string email, 
            int accountId, Role role, bool isActive, string? licenseNumber = null)
        {
            UserId=userId;
            Name=name;
            Surname=surname;
            Email=email;
            AccountId=accountId;
            Role=role;
            IsActive=isActive;
            LicenseNumber=licenseNumber;
        }

        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public int AccountId { get; set; }
        public Role Role { get; set; }
        public bool IsActive { get; set; }

        // doctor only
        public string? LicenseNumber { get; set; }
    }
}
