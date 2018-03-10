namespace Product.Presenters.Dtos
{
    public class IndexQuantityDto
    {
        public string Variant { get; set; }
        public string Store { get; set; }
        public int Quantity { get; set; }
        public int LowQuantity { get; set; }
    }
}
