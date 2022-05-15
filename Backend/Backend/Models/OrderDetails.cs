namespace Backend.Models
{
    public class OrderDetails
    {
        public Product Product { get; set; }
        public Order Order { get; set; }
        public uint Quantity { get; set; }
        public double Price
        {
            get
            {
                return Quantity * Product.Price;
            }
        }
    }
}
