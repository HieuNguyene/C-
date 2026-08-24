using Microsoft.Extensions.Logging;
namespace W4.Service.DTOs.Request
{
    public class PaginationRequest
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;

    }
}





