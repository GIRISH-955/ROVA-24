namespace ROVA_24.Model
{
    public class Address
    {
        public int AddressId {  get; set; }
        public int CustomerId {  get; set; }
        public String Street {  get; set; }
        public String City {  get; set; }
        public String State {  get; set; }
        public String Country {  get; set; }
        public String ZipCode {  get; set; }

        public Customer Customer { get; set; }

    }
}
