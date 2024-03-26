using System.ComponentModel.DataAnnotations;

namespace ROVA_24.DTO.AddressDTO
{
    public class AddressRequestDTO : IValidatableObject
    {
        public string Name { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Phone number must be 10 digits")]
        public string PhoneNumber { get; set; }

        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Alternate phone number must be 10 digits")]
        public string? AlternatePhoneNumber { get; set; }
        [Required(ErrorMessage = "Street is required")]
        public string Street { get; set; }
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }
        [Required(ErrorMessage = "State is required")]
        public string State { get; set; }
        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; }
        [Required(ErrorMessage = "Zipcode is required")]
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Zipcode is not valid")]
        public string Zipcode { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (!string.IsNullOrEmpty(AlternatePhoneNumber) && string.Equals(PhoneNumber, AlternatePhoneNumber, StringComparison.OrdinalIgnoreCase))
            {
                results.Add(new ValidationResult("Alternate phone number cannot be the same as phone number", new[] { nameof(AlternatePhoneNumber) }));
            }

            return results;
        }
    }
}
