namespace ProductManagment.DTOs.Request
{
    public class ProductSearchRequest
    {
        public string KeyWord { get; set; } = string.Empty;
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
