using Microsoft.Extensions.Logging;
namespace W4.Service.DTOs.Request
{
    public class StudentQueryRequest : PaginationRequest
    {
        public string? Keyword { get; set; }

    }
}






