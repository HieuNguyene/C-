using W4.enums;

namespace W3.DTOs.Respones
{
    public class StudentResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime Dob { get; set; }
        public GenderType Gender { get; set; }
        public string? ClassId { get; set; } = string.Empty;
    }
}
