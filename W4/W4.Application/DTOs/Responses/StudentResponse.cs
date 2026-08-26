using W4.Domain.Enums;
using System;
namespace W4.Application.DTOs
{
    public class StudentResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime Dob { get; set; }
        public GenderType Gender { get; set; }
        public string? ClassId { get; set; }
    }
}

