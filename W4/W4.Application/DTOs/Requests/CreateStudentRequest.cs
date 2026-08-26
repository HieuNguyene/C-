using W4.Application.DTOs;
using W4.Application.Validations;
using W4.Infrastructure.Repositories.Interfaces;
using W4.Domain.Enums;
using System;
namespace W4.Application.DTOs
{
    public class CreateStudentRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime Dob { get; set; }
        public GenderType Gender { get; set; }
        public string? ClassId { get; set; }
    }
}






