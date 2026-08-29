using W4.Application.DTOs;
using W4.Application.Validations;
using W4.Application.Interfaces;
using System;
namespace W4.Application.DTOs
{
    public class CreateScoreRequest
    {
        public Guid Id { get; set; }
        public float Value { get; set; }
        public Guid StudentId { get; set; }
        public string SubjectId { get; set; } = string.Empty;
    }
}







