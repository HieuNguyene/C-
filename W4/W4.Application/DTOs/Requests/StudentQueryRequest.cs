using W4.Application.DTOs;
using W4.Application.Validations;
using W4.Infrastructure.Repositories.Interfaces;
namespace W4.Application.DTOs
{
    public class StudentQueryRequest
    {
        public string? Keyword { get; set; }
        public int PageSize { get; set; } = 10;
        public int Page { get; set; } = 1;
    }
}







