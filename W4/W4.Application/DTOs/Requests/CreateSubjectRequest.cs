using W4.Application.DTOs;
using W4.Application.Validations;
using W4.Application.Interfaces;
namespace W4.Application.DTOs
{
    public class CreateSubjectRequest
    {
        public string SubjectId { get; set; } = string.Empty;
        public string SubjectName { get; set; } = string.Empty;
    }
}






