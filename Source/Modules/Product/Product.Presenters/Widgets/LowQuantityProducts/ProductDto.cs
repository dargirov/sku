namespace Product.Presenters.Widgets.LowQuantityProducts
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string StoreName { get; set; }

        public int Quantity { get; set; }

        public int LowQuantity { get; set; }
    }
}
