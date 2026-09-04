using System.Data;
using Dapper;
using W4.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using W4.Infrastructure.Data;
using W4.Domain.Entities;

namespace W4.Infrastructure.Repositories.Implementations
{
    public class ClassRepository : IClassRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IDbConnection _dbConnection;

        public ClassRepository(ApplicationDbContext context, IDbConnection dbConnection)
        {
            _context = context;
            _dbConnection = dbConnection;
        }


        public async Task<Class> CreateAsync(Class @class)
        {
            _context.Add(@class);
            await _context.SaveChangesAsync();
            return @class;
        }

        public async Task<bool> DeleteByIdAsync(string classId)
        {
            Class? @class = await GetByIdAsync(classId);
            if (@class == null) return false;

            _context.Remove(@class);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(Class newClass)
        {
            _context.Update(newClass);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Class>> GetAllClassAsync()
        {
            var sql = "SELECT * FROM Classes";
            var classes = await _dbConnection.QueryAsync<Class>(sql);
            return classes.ToList();
        }

        public async Task<Class?> GetByIdAsync(string classId)
        {
            var sp_GetClassById = "sp_GetClassById";
            return await _dbConnection.QueryFirstOrDefaultAsync<Class>(
                sp_GetClassById,
                new { ClassId = classId },
                commandType: CommandType.StoredProcedure);
        }
        public async Task<int> CountStudentsInClassAsync(string classId)
        {
            var parameters = new DynamicParameters();

            parameters.Add("ClassId", classId);

            parameters.Add("TotalCount", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await _dbConnection.ExecuteAsync(
                "sp_CountStudentsInClass",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            int result = parameters.Get<int>("TotalCount");
            return result;
        }
    }
}
