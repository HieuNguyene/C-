using W3.DTOs;
using W3.Responses;
using W4.Interface;
using W4.model;
using W4.Repository;

namespace W4.Service
{
    public class ScoreService : IScoreService
    {
        private readonly IScoreRepository _repository;
        private readonly ILogger<ScoreService> _logger;
        public ScoreService(IScoreRepository repository, ILogger<ScoreService> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task<ApiResponse<Score>> CreateAsync(CreateScoreRequest request)
        {
            _logger.LogInformation("Tạo điểm mới ");
            Score? scoreIsDuplicate = await _repository.GetSpecificScoreAsync(request.StudentId, request.SubjectId);
            if (scoreIsDuplicate != null)
            {
                _logger.LogWarning("Sinh viên đã có điểm không thể thêm mới");
                throw new InvalidOperationException("Sinh viên này đã có điểm môn này! Vui lòng dùng chức năng Cập nhật (Update).");
            }
            Score score = new Score(request.Id, request.Value, request.StudentId, request.SubjectId);
            var Data = await _repository.CreateAsync(score);
            _logger.LogInformation("Đã thêm điểm thành công");
            return new ApiResponse<Score>
            {
                Success = true,
                Message = "Đã thêm điểm thành công",
                Data = Data
            };
        }

        public async Task<ApiResponse<bool>> DeleteAsync(Guid id)
        {
            _logger.LogInformation("Xóa điểm ");
            if (id == Guid.Empty)
            {
                _logger.LogWarning("Mã không hợp lệ");
                throw new ArgumentNullException("Mã không hợp lệ");
            }
            Score? score = await _repository.GetByIdAsync(id);
            if (score == null)
            {
                _logger.LogWarning("Không tìm tìm thấy điểm trong hệ thống");
                throw new KeyNotFoundException("Không có điểm số này trong hệ thống");
            }
            bool result = await _repository.DeleteAsync(id);
            _logger.LogInformation("Đã xóa thành công điểm ");
            return new ApiResponse<bool>
            {
                Success = true,
                Message = "Đã xóa thành công ",
                Data = result
            };
        }

        public async Task<ApiResponse<List<Score>>> GetScoreByStudentAsync(Guid studentId)
        {
            _logger.LogInformation("Lấy danh sách điểm của sinh viên studentId={studenId}", studentId);
            if (studentId == Guid.Empty)
            {
                _logger.LogWarning("Mã sinh viên không hợp lệ");
                throw new ArgumentNullException("Mã sinh viên không hợp lệ");
            }
            List<Score> scores = await _repository.GetScoresByStudentAsync(studentId);
            _logger.LogInformation("Lấy danh sách điểm của sinh viên thành công");
            return new ApiResponse<List<Score>>
            {
                Success = true,
                Message = "Đã lấy danh sách điểm thành công",
                Data = scores
            };
        }

        public async Task<ApiResponse<List<Score>>> GetScoreBySubjectAsync(string subjectId)
        {
            _logger.LogInformation("Lấy danh sách điểm của môn học subjecId={subjectId}", subjectId);
            if (String.IsNullOrWhiteSpace(subjectId))
            {
                _logger.LogWarning("Mã môn học không đúng");
                throw new ArgumentNullException("Mã môn học không hợp lệ");
            }
            List<Score> scores = await _repository.GetScoreBySubjectAsync(subjectId);
            _logger.LogInformation("Đã lấy danh sách điểm thành công");
            return new ApiResponse<List<Score>>
            {
                Success = true,
                Message = "Đã lấy danh sách điểm thành công",
                Data = scores
            };
        }

        public async Task<ApiResponse<bool>> UpdateAsync(Guid scoreId, UpdateScoreRequest request)
        {
            _logger.LogInformation("Cập nhật điểm mới");
            if (scoreId == Guid.Empty)
            {
                throw new ArgumentException("Mã điểm số không hợp lệ!");
            }

            Score? scoreToUpdate = await _repository.GetByIdAsync(scoreId);
            if (scoreToUpdate == null)
            {
                throw new KeyNotFoundException("Không tìm thấy điểm số này trong hệ thống.");
            }

            scoreToUpdate.UpdateValue(request.Value);
            var result = await _repository.UpdateAsync(scoreToUpdate);
            
            _logger.LogInformation("Đã cập nhật điểm thành công");
            return new ApiResponse<bool>
            {
                Success = true,
                Message = "Đã cập nhật điểm thành công",
                Data = result
            };
        }
    }
}