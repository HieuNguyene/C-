namespace W4.model
{
    public class Subject
    {
        public string SubjectId {get;private set;}
        public string SubjectName{get;private set;}

        private readonly List<Score> _scores = new List<Score>();
        public  IReadOnlyCollection<Score> Scores => _scores.AsReadOnly();

        protected Subject()
        {
            SubjectId = string.Empty;
            SubjectName = string.Empty; 
        }
        public Subject(string subjectId,string subjectName)
        {
            SubjectId = subjectId;
            SubjectName = subjectName;
        }

    }
}