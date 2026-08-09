namespace ProductManagment.DTOs.Request
{
    public class ProductCreateRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
    }
}
