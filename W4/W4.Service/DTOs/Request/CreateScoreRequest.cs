using Microsoft.Extensions.Logging;
namespace W4.Service.DTOs
{
    public class CreateScoreRequest
    {
        public Guid Id{get;set;}
        public float Value {get;set;}

        public Guid StudentId {get; set;}
        public string SubjectId {get;set;}= String.Empty;
    }
}




