using W4.Application.DTOs;
using W4.Application.Validations;
using W4.Infrastructure.Repositories.Interfaces;
using W4.Domain.Enums;
using System;
namespace W4.Application.DTOs
{
    public class UpdateStudentRequest
    {
        public string? Name { get; set; }
        public DateTime? Dob { get; set; }
        public GenderType? Gender { get; set; }
        public string? ClassId { get; set; }
    }
}







