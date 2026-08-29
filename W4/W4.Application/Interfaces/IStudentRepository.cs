using W4.Application.Interfaces;
using W4.Domain.Entities;

namespace W4.Application.Interfaces
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












