using System.ComponentModel.DataAnnotations;

namespace Database.Patients
{
    public class Address
    {
        public int AddressId { get; set; }
        public string City { get; set; }
        public string Street { get; set; }

        [MaxLength(10)]
        public string HouseNumber { get; set; }

        [MaxLength(10)]
        public string? RoomNumber { get; set; }

        public Address(string city, string street, string houseNumber, string? roomNumber)
        {
            City = city;
            Street = street;
            HouseNumber = houseNumber;
            RoomNumber = roomNumber;
        }
    }
}
