using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ROVA_24.Models
{
    public class Customers
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string AlternatePhoneNumber { get; set; }
        public string Password { get; set; }
        public string Enable {  get; set; }
        public virtual ICollection<Address> Addresses { get; set; } = new List<Address>(); // Navigation property 

        public ICollection<Reviews> Reviews { get; set; }
    }
}

