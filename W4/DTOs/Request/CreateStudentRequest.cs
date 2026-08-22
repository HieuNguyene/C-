using W3.model;
using W4.enums;

namespace W3.DTOs.Request
{
    public class CreateStudentRequest
    {
        public string Name { get; set; } = string.Empty;
        public DateTime Dob { get; set; }
        public GenderType Gender { get; set; }
        public string? ClassId { get; set; }
    }
}
