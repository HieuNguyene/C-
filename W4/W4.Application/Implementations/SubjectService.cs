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
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _repository;
        private readonly ILogger<SubjectService> _logger;

        public SubjectService(ISubjectRepository repository, ILogger<SubjectService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<ApiResponse<Subject>> CreateAsync(CreateSubjectRequest request)
        {
            _logger.LogInformation("Đang tạo môn học mới: {SubjectId}", request.SubjectId);

            // Kiểm tra môn học đã tồn tại chưa
            var existingSubject = await _repository.GetByIdAsync(request.SubjectId);
            if (existingSubject != null)
            {
                _logger.LogWarning("Mã môn học {SubjectId} đã tồn tại", request.SubjectId);
                throw new InvalidOperationException("Mã môn học này đã tồn tại trong hệ thống!");
            }

            Subject subject = new Subject(request.SubjectId, request.SubjectName);
            await _repository.CreateAsync(subject);

            _logger.LogInformation("Tạo môn học thành công: {SubjectId}", request.SubjectId);
            return new ApiResponse<Subject>
            {
                Success = true,
                Message = "Tạo môn học thành công",
                Data = subject
            };
        }

        public async Task<ApiResponse<bool>> DeleteAsync(string subjectId)
        {
            _logger.LogInformation("Đang xóa môn học: {SubjectId}", subjectId);

            if (string.IsNullOrWhiteSpace(subjectId))
            {
                throw new ArgumentException("Mã môn học không hợp lệ!");
            }

            Subject? subject = await _repository.GetByIdAsync(subjectId);
            if (subject == null)
            {
                throw new KeyNotFoundException("Không tìm thấy môn học này!");
            }

            // NOTE: Nếu môn học có điểm số, Database có thể văng lỗi Foreign Key khi xóa. 
            // Có thể bổ sung check _scoreRepository ở đây nếu cần thiết, 
            // nhưng tạm thời EF Core sẽ lo việc báo lỗi nếu dính FK.

            bool isDeleted = await _repository.DeleteAsync(subjectId);

            _logger.LogInformation("Xóa môn học thành công: {SubjectId}", subjectId);
            return new ApiResponse<bool>
            {
                Success = true,
                Message = "Xóa môn học thành công",
                Data = isDeleted
            };
        }

        public async Task<ApiResponse<List<Subject>>> GetAllAsync()
        {
            _logger.LogInformation("Đang lấy danh sách tất cả môn học");

            var subjects = await _repository.GetAllAsync();

            return new ApiResponse<List<Subject>>
            {
                Success = true,
                Message = "Lấy danh sách môn học thành công",
                Data = subjects
            };
        }

        public async Task<ApiResponse<Subject>> GetByIdAsync(string subjectId)
        {
            _logger.LogInformation("Lấy thông tin môn học: {SubjectId}", subjectId);

            if (string.IsNullOrWhiteSpace(subjectId))
            {
                throw new ArgumentException("Mã môn học không hợp lệ!");
            }

            var subject = await _repository.GetByIdAsync(subjectId);
            if (subject == null)
            {
                throw new KeyNotFoundException("Không tìm thấy môn học này!");
            }

            return new ApiResponse<Subject>
            {
                Success = true,
                Message = "Tìm thấy môn học",
                Data = subject
            };
        }

        public async Task<ApiResponse<bool>> UpdateAsync(string subjectId, UpdateSubjectRequest request)
        {
            _logger.LogInformation("Đang cập nhật môn học: {SubjectId}", subjectId);

            if (string.IsNullOrWhiteSpace(subjectId))
            {
                throw new ArgumentException("Mã môn học không hợp lệ!");
            }

            Subject? subject = await _repository.GetByIdAsync(subjectId);
            if (subject == null)
            {
                throw new KeyNotFoundException("Không tìm thấy môn học này!");
            }

            subject.UpdateSubjectName(request.SubjectName);
            await _repository.UpdateAsync(subject);

            _logger.LogInformation("Cập nhật môn học thành công: {SubjectId}", subjectId);
            return new ApiResponse<bool>
            {
                Success = true,
                Message = "Cập nhật môn học thành công",
                Data = true
            };
        }
    }
}












