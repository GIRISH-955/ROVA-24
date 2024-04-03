namespace ROVA_24.Model
{
    public class OrderItem
    {
        public int OrderItemId {  get; set; }
        public int OrderId {  get; set; }
        public string ProductId { get; set; } //SKU
        public int Quantity {  get; set; }
        public double Price {  get; set; }
        public Order Order { get; set; }


    }
}
