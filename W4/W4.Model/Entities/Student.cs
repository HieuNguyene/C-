using W4.Model.Enums;
using W4.Model.Entities;

namespace W4.Model.Entities
{
    public class Student
    {
        public Guid Id { get; set; }
        public string Name { private set; get; } = string.Empty;
        public DateTime DateOfBirth { get; private set; }
        public GenderType Gender { get; private set; }

        //liên hệ với lớp học
        public string? ClassId { get; private set; }
        public Class? Class { get; private set; }//Navigation property trỏ về Class

        // liên hệ với điểm số
        private readonly List<Score> _scores = new List<Score>();
        public IReadOnlyCollection<Score> Scores => _scores.AsReadOnly();

        protected Student()
        {
            Name = string.Empty;
            ClassId = string.Empty;
        }
        public Student(Guid id, string name, DateTime dob, GenderType gender, string? classId)
        {
            Id = id;
            Name = name;
            DateOfBirth = dob;
            Gender = gender;
            ClassId = classId;
        }
        public void TransferToClass(String newClassId)
        {

            if (string.IsNullOrWhiteSpace(newClassId))
            {
                throw new ArgumentNullException("Mã lớp không hợp lệ");
            }
            ClassId = newClassId;
        }
        public void ChangeName(String newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
            {
                throw new ArgumentNullException("Tên không hợp lệ");
            }
            Name = newName;
        }
        public void ChangeDob(DateTime newDob)
        {
            if (string.IsNullOrWhiteSpace(newDob.ToString()))
            {
                throw new ArgumentNullException("Ngày tháng năm sinh không hợp lệ");
            }
            DateOfBirth = newDob;
        }
        public void ChangeGender(GenderType newGender)
        {
            if (!Enum.IsDefined(typeof(GenderType), newGender))
            {
                throw new ArgumentNullException("Giới không hợp lệ");
            }
            Gender = newGender;
        }
    }

}


