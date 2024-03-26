using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ROVA_24.Models
{
    public class Reviews
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int reviewId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public string Comment { get; set; }
        public string Rating { get; set; }

        public Customers Customers { get; set; }
        public Products Products { get; set; }
    }
}
