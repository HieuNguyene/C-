using W4.Application.DTOs;
using W4.Application.Validations;
using W4.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using W4.Application.DTOs;
using W4.Infrastructure.Repositories.Interfaces;
using W4.Application.Interfaces;
using W4.Domain.Entities;

namespace W4.Application.Implementations
{
    public class ClassService : IClassService
    {
        private readonly IClassRepository _repository;
        private readonly ILogger<ClassService> _logger;
        public ClassService(IClassRepository repository, ILogger<ClassService> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task<ApiResponse<Class>> CreateAsync(CreateClassRequest newClass)
        {
            _logger.LogInformation("Tạo lớp học mới ");
            Class @class = new Class(newClass.ClassId, newClass.ClassName);
            await _repository.CreateAsync(@class);
            _logger.LogInformation("Đã tạo thành công lớp học ID={id}", @class.ClassId);
            return new ApiResponse<Class>
            {
                Success = true,
                Message = $"Đã thành công tạo lớp học {@class.ClassId}",
                Data = @class
            };
        }

        public async Task<ApiResponse<bool>> DeleteAsync(string id)
        {
            _logger.LogInformation("Xóa một lớp học ");
            if (String.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("ID lớp học không hợp lệ");
            }
            Class? @class = await _repository.GetByIdAsync(id);
            if (@class == null)
            {
                _logger.LogWarning("Không tìm thấy lớp học Id ={id}", id);
                throw new KeyNotFoundException("Không tìm thấy lớp học này");
            }
            await _repository.DeleteByIdAsync(id);
            _logger.LogInformation("Đã xóa thành công lớp học Name ={@class.ClassName}", @class.ClassName);
            return new ApiResponse<bool>
            {

                Success = true,
                Message = $"Đã xóa thành công lớp học Name ={@class.ClassName}",
                Data = true
            };
        }

        public async Task<ApiResponse<List<Class>>> GetAllAsync()
        {
            _logger.LogInformation("Lấy danh sách lớp học");
            List<Class> classes = await _repository.GetAllClassAsync();
            _logger.LogInformation("Lấy danh sách lớp học thành công");
            return new ApiResponse<List<Class>>
            {
                Success = true,
                Message = "Đã lấy danh sách lớp học thành công!",
                Data = classes
            };
        }

        public async Task<ApiResponse<Class>> GetByIdAsync(string ClassId)
        {
            _logger.LogInformation("Lấy lớp học có mã Id={id}", ClassId);
            if (String.IsNullOrWhiteSpace(ClassId))
            {
                _logger.LogWarning("Mã lớp học không hợp lệ");
                throw new ArgumentException("Mã lớp học không hợp lệ");
            }
            Class? @class = await _repository.GetByIdAsync(ClassId);
            if (@class == null)
            {
                _logger.LogWarning("Không tìm thấy lớp học Id ={id}", ClassId);
                throw new KeyNotFoundException("Không tìm thấy lớp học này");
            }
            return new ApiResponse<Class>
            {
                Success = true,
                Message = $"Đã tìm thấy lớp học {ClassId}",
                Data = @class
            };
        }

        public async Task<ApiResponse<bool>> UpdateAsync(string id, CreateClassRequest newClassRequest)
        {
            _logger.LogInformation("Cập nhật thông tin cho lớp học Id={id}", id);
            if (String.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Mã lớp học không hợp lệ");
                throw new ArgumentException("Mã lớp học không hợp lệ");
            }
            Class? @class = await _repository.GetByIdAsync(id);
            if (@class == null)
            {
                _logger.LogWarning("Không tìm thấy lớp học Id ={id}", id);
                throw new KeyNotFoundException("Không tìm thấy lớp học này");
            }
            @class.UpdateClassName(newClassRequest.ClassName);
            await _repository.UpdateAsync(@class);
            return new ApiResponse<bool>
            {
                Success = true,
                Message = "Đã cập nhật thông tin thành công",
                Data = true
            };
        }
    }
}











