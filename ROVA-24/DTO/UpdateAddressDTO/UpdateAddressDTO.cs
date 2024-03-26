namespace ROVA_24.DTO.UpdateAddressDTO
{
    public class UpdateAddressDTO
    {
        public int CustomerId { get; set; }//Foreign key 
        public string Name { get; set; }
        //public string PhoneNumber { get; set; }
        public string? AlternatePhoneNumber { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Zipcode { get; set; }
    }
}
