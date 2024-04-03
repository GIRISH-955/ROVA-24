namespace ROVA_24.Model
{
    public class Order
    {
        public int OrderId {  get; set; }
        public int CustomerId {  get; set; }
        public String Status {  get; set; }
        public String OrderDate {  get; set; }
        public DateTime Ordertime {  get; set; }
        public Customer Customer { get; set; }  
    public List<OrderItem> OrderItems {  get; set; }

    }
}
