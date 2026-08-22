using W3.model;
using W3.Responses;

namespace W4.Interface
{
    public interface IStudentRepository
    {
        Task<Student> CreateStudentAsync(Student student);
        Task<List<Student>> GetAllStudentAsync();
        Task<Student?> GetByIdAsync(Guid id);
        Task<List<Student>> GetStudentByKeyWordAsync(string? keyword, int pageSize, int pageNumber);
        Task<Student> UpdateAsync(Student newStudent);
        Task<bool> DeleteByIdAsync(Guid id);
    }
}