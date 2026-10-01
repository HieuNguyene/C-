using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using W4.Application.DTOs;
using W4.Application.DTOs.Responses;
using W4.Application.Features.Students.Commands;
using W4.Application.Features.Students.Queries;

namespace W4.API.Controllers
{
    [Authorize]
    [Route("api/student")]
    [ApiController]
    [SwaggerTag("Quản lý học sinh (Student Management)")]
    public class StudentController(MediatR.IMediator mediator) : ApiControllerBase
    {
        /// <summary>
        /// Tìm kiếm học sinh theo từ khóa và phân trang
        /// </summary>
        /// <remarks>
        /// Yêu cầu Policy: **CanManageStudents** (Admin hoặc Teacher).
        /// Hỗ trợ tìm kiếm theo tên hoặc mã học sinh, có phân trang.
        /// </remarks>
        /// <param name="query">Tham số tìm kiếm (Keyword, Page, PageSize)</param>
        /// <response code="200">Lấy danh sách học sinh thành công</response>
        /// <response code="401">Chưa xác thực (Thiếu JWT Bearer token)</response>
        /// <response code="403">Không đủ quyền truy cập (Chỉ Admin và Teacher)</response>
        [HttpGet("search")]
        [Authorize(Policy = "CanManageStudents")]
        [SwaggerOperation(Summary = "Tìm kiếm học sinh", Description = "Tìm kiếm học sinh theo từ khóa với phân trang")]
        [ProducesResponseType(typeof(ApiResponse<List<StudentResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetByKeyWordAsync([FromQuery] GetStudentByKeyWordQuery query)
        {
            var response = await mediator.Send(query);
            return HandleResult(response);
        }

        /// <summary>
        /// Thêm mới một học sinh vào hệ thống
        /// </summary>
        /// <remarks>
        /// Yêu cầu Policy: **AdminOnly** (Chỉ Admin).
        /// Mã lớp (ClassId) nếu cung cấp thì lớp học đó phải tồn tại trước trong hệ thống.
        /// </remarks>
        /// <param name="command">Thông tin học sinh bao gồm Tên, Ngày sinh, Giới tính, Lớp học</param>
        /// <response code="200">Tạo học sinh thành công</response>
        /// <response code="400">Dữ liệu học sinh không hợp lệ (Validation thất bại hoặc lớp không tồn tại)</response>
        /// <response code="401">Chưa xác thực JWT</response>
        /// <response code="403">Không đủ quyền truy cập (Chỉ Admin)</response>
        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        [SwaggerOperation(Summary = "Tạo mới học sinh", Description = "Thêm mới một hồ sơ học sinh (Chỉ Admin)")]
        [ProducesResponseType(typeof(ApiResponse<StudentResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<StudentResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateStudentCommand command)
        {
            var student = await mediator.Send(command);
            return HandleResult(student);
        }

        /// <summary>
        /// Lấy thông tin chi tiết một học sinh theo ID
        /// </summary>
        /// <remarks>
        /// Yêu cầu Policy: **CanManageStudents** (Admin hoặc Teacher).
        /// </remarks>
        /// <param name="id">Mã định danh duy nhất (Guid) của học sinh</param>
        /// <response code="200">Tìm thấy thông tin học sinh</response>
        /// <response code="404">Không tìm thấy học sinh với ID tương ứng</response>
        /// <response code="401">Chưa xác thực JWT</response>
        /// <response code="403">Không đủ quyền truy cập</response>
        [HttpGet("{id}")]
        [Authorize(Policy = "CanManageStudents")]
        [SwaggerOperation(Summary = "Xem chi tiết học sinh", Description = "Lấy hồ sơ chi tiết của học sinh theo Guid")]
        [ProducesResponseType(typeof(ApiResponse<StudentResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<StudentResponse>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var query = new GetStudentByIdQuery(id);
            var result = await mediator.Send(query);
            return HandleResult(result);
        }

        /// <summary>
        /// Lấy danh sách tất cả học sinh thuộc một lớp học
        /// </summary>
        /// <remarks>
        /// Yêu cầu Policy: **CanManageStudents** (Admin hoặc Teacher).
        /// </remarks>
        /// <param name="classId">Mã lớp học (ví dụ: '10A1', '12B2')</param>
        /// <response code="200">Lấy danh sách học sinh theo lớp thành công</response>
        /// <response code="401">Chưa xác thực JWT</response>
        /// <response code="403">Không đủ quyền truy cập</response>
        [HttpGet("class/{classId}")]
        [Authorize(Policy = "CanManageStudents")]
        [SwaggerOperation(Summary = "Danh sách học sinh theo lớp", Description = "Lấy toàn bộ học sinh đang theo học tại một lớp")]
        [ProducesResponseType(typeof(ApiResponse<List<StudentResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetStudentsByClassIdAsync(string classId)
        {
            var query = new GetStudentsByClassIdQuery(classId);
            var result = await mediator.Send(query);
            return HandleResult(result);
        }

        /// <summary>
        /// Cập nhật thông tin một học sinh
        /// </summary>
        /// <remarks>
        /// Yêu cầu Policy: **CanManageStudents** (Admin hoặc Teacher).
        /// </remarks>
        /// <param name="id">Mã ID của học sinh cần cập nhật</param>
        /// <param name="command">Thông tin chỉnh sửa</param>
        /// <response code="200">Cập nhật thành công</response>
        /// <response code="400">Dữ liệu cập nhật không hợp lệ</response>
        /// <response code="404">Không tìm thấy học sinh để cập nhật</response>
        /// <response code="401">Chưa xác thực JWT</response>
        /// <response code="403">Không đủ quyền truy cập</response>
        [HttpPut("{id}")]
        [Authorize(Policy = "CanManageStudents")]
        [SwaggerOperation(Summary = "Cập nhật học sinh", Description = "Chỉnh sửa thông tin của học sinh hiện có")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateStudentCommand command)
        {
            command.Id = id;
            var result = await mediator.Send(command);
            return HandleResult(result);
        }

        /// <summary>
        /// Xóa vĩnh viễn một học sinh khỏi hệ thống
        /// </summary>
        /// <remarks>
        /// Yêu cầu Policy: **AdminOnly** (Chỉ Admin mới có quyền xóa học sinh).
        /// </remarks>
        /// <param name="id">Mã ID của học sinh cần xóa</param>
        /// <response code="200">Xóa học sinh thành công</response>
        /// <response code="404">Không tìm thấy học sinh cần xóa</response>
        /// <response code="401">Chưa xác thực JWT</response>
        /// <response code="403">Không đủ quyền truy cập (Chỉ Admin)</response>
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        [SwaggerOperation(Summary = "Xóa học sinh", Description = "Xóa hồ sơ học sinh theo ID (Chỉ Admin)")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteById(Guid id)
        {
            var command = new DeleteStudentCommand(id);
            var result = await mediator.Send(command);
            return HandleResult(result);
        }
    }
}










