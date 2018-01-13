namespace Product.Bll.Dtos.Api
{
    public class ProductDto
    {
        public string Name { get; set; }
        public string Store { get; set; }
        public string Code { get; set; }
        public decimal PriceNet { get; set; }
        public decimal PriceCustomer { get; set; }
        public int Quantity { get; set; }
        public string StoreId { get; set; }
    }
}
