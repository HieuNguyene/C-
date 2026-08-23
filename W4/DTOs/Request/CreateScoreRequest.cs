namespace W3.DTOs
{
    public class CreateScoreRequest
    {
        public Guid Id{get;set;}
        public float Value {get;set;}

        public Guid StudentId {get; set;}
        public string SubjectId {get;set;}= String.Empty;
    }
}