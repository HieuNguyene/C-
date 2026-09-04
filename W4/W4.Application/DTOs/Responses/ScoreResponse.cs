using System;

namespace W4.Application.DTOs.Responses
{
    public class ScoreResponse
    {
        public Guid Id { get; set; }
        public float Value { get; set; }
        public Guid StudentId { get; set; }
        public string SubjectId { get; set; } = string.Empty;
    }
}
