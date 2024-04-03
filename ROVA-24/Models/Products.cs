using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ROVA_24.Models
{
    public class Products
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ProductId { get; set; }
        public string Sku { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<string>? Images { get; set; }
        public int StockQuantity { get; set; }
        public ICollection<Reviews> Reviews { get; set; }
        //public List<ProductVarients> Variants { get; set; }
    }
}
