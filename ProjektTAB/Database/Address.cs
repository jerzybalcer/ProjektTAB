namespace Database
{
    public class Address
    {
        public string City { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }
        public int? RoomNumber { get; set; }

        public Address(string city, string street, int houseNumber, int? roomNumber)
        {
            City = city;
            Street = street;
            HouseNumber = houseNumber;
            RoomNumber = roomNumber;
        }
    }
}
