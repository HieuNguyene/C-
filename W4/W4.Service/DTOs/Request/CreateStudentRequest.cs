using Microsoft.Extensions.Logging;
using W4.Model.Entities;
using W4.Model.Enums;

namespace W4.Service.DTOs.Request
{
    public class CreateStudentRequest
    {
        public string Name { get; set; } = string.Empty;
        public DateTime Dob { get; set; }
        public GenderType Gender { get; set; }
        public string? ClassId { get; set; }
    }
}





