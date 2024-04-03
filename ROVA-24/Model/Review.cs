namespace ROVA_24.Model
{
    public class Review
    {
        public int ReviewId {  get; set; }
        public int CustomerId { get; set; } // Foreign key referencing Customer
        public int ProductId { get; set; } // Foreign key referencing Product
        public String Comment {  get; set; }
        public int Rating { get; set; } // Rating out of 5 stars
        public Customer Customer { get; set; }  

    }
}
