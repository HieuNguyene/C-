using W4.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using W4.Domain.Entities;
using W4.Infrastructure.Data;

namespace W4.Infrastructure.Repositories.Implementations
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;
        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Student> CreateStudentAsync(Student student)
        {
            _context.Add(student);
            await _context.SaveChangesAsync();
            return student;
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            var student = await GetByIdAsync(id);
            if (student == null)
            {
                return false;
            }
            _context.Remove(student);
            await _context.SaveChangesAsync();
            return true;
        }

        public Task<List<Student>> GetAllStudentAsync()
        {
            return _context.Students.AsNoTracking().ToListAsync();
        }

        public async Task<Student?> GetByIdAsync(Guid id)
        {
            return await _context.Students.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Student>> GetStudentByKeyWordAsync(string? keyword, int pageSize, int pageNumber)
        {
            var query = _context.Students.AsQueryable();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(s => s.Name.Contains(keyword));
            }
            var students = await query
                            .OrderBy(x => x.Id)
                            .Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize)
                            .ToListAsync();
            return students;
        }

        public async Task<Student> UpdateAsync(Student newStudent)
        {
            _context.Students.Update(newStudent);
            await _context.SaveChangesAsync();
            return newStudent;
        }
    }
}











