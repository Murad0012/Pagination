namespace P335_BackEnd.Entities
{
    public class Sale
    {
        public int Id { get; set; }
        public byte Discount { get; set; }
        public List<Product>? Products { get; set; }
    }
}
