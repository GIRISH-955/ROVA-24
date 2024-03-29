
namespace ROVA_24.DTO
{
    public class CustomerDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Enable { get; set; } = true;
        public string PhoneNumber { get; set; }
        public List<AddressDTO> Addresses { get; set; }

    }
}
