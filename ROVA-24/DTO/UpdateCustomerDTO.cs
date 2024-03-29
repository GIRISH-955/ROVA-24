namespace ROVA_24.DTO
{
    public class UpdateCustomerDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<UpdateAddressDTO> Addresses { get; set; }
    }
}
