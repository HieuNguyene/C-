using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using W4.Application.DTOs;
using W4.Application.DTOs.Responses;
using W4.Application.Features.Subjects.Commands;
using W4.Application.Features.Subjects.Queries;

namespace W4.API.Controllers
{
    [Authorize]
    [Route("api/subject")]
    [ApiController]
    [SwaggerTag("Quản lý môn học (Subject Management)")]
    public class SubjectController(IMediator mediator) : ApiControllerBase
    {
        /// <summary>
        /// Lấy danh sách tất cả các môn học
        /// </summary>
        /// <remarks>
        /// Dữ liệu môn học được lưu trong **In-Memory Cache** để tối ưu tốc độ đọc.
        /// Bất kỳ người dùng đã xác thực đều có thể gọi API này.
        /// </remarks>
        /// <response code="200">Lấy danh sách môn học thành công</response>
        /// <response code="401">Chưa xác thực JWT Bearer</response>
        [HttpGet]
        [SwaggerOperation(Summary = "Danh sách môn học", Description = "Lấy tất cả các môn học hiện có (sử dụng In-Memory Cache)")]
        [ProducesResponseType(typeof(ApiResponse<List<SubjectResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAll() => Ok(await mediator.Send(new GetAllSubjectsQuery()));

        /// <summary>
        /// Lấy thông tin chi tiết một môn học theo mã môn
        /// </summary>
        /// <param name="id">Mã môn học (ví dụ: 'MATH', 'ENG')</param>
        /// <response code="200">Tìm thấy môn học</response>
        /// <response code="401">Chưa xác thực JWT Bearer</response>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Chi tiết môn học", Description = "Lấy thông tin môn học theo mã môn")]
        [ProducesResponseType(typeof(ApiResponse<SubjectResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetById(string id) => Ok(await mediator.Send(new GetSubjectByIdQuery(id)));

        /// <summary>
        /// Tạo mới một môn học
        /// </summary>
        /// <remarks>
        /// Yêu cầu Policy: **AdminOnly**. Khi tạo thành công, In-Memory Cache sẽ được xóa để đồng bộ dữ liệu mới.
        /// </remarks>
        /// <param name="command">Thông tin môn học (Mã môn, Tên môn)</param>
        /// <response code="200">Tạo môn học thành công</response>
        /// <response code="400">Mã môn học đã tồn tại hoặc dữ liệu không hợp lệ</response>
        /// <response code="401">Chưa xác thực JWT</response>
        /// <response code="403">Không đủ quyền truy cập (Chỉ Admin)</response>
        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        [SwaggerOperation(Summary = "Tạo môn học mới", Description = "Thêm một môn học mới vào hệ thống (Chỉ Admin)")]
        [ProducesResponseType(typeof(ApiResponse<SubjectResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<SubjectResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Create([FromBody] CreateSubjectCommand command) => Ok(await mediator.Send(command));

        /// <summary>
        /// Cập nhật thông tin một môn học
        /// </summary>
        /// <remarks>
        /// Yêu cầu Policy: **AdminOnly**.
        /// </remarks>
        /// <param name="id">Mã môn học cần cập nhật</param>
        /// <param name="command">Dữ liệu tên môn học mới</param>
        /// <response code="200">Cập nhật môn học thành công</response>
        /// <response code="400">Dữ liệu không hợp lệ hoặc môn học không tồn tại</response>
        /// <response code="401">Chưa xác thực JWT</response>
        /// <response code="403">Không đủ quyền truy cập (Chỉ Admin)</response>
        [HttpPut("{id}")]
        [Authorize(Policy = "AdminOnly")]
        [SwaggerOperation(Summary = "Cập nhật môn học", Description = "Chỉnh sửa tên môn học (Chỉ Admin)")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateSubjectCommand command)
        {
            command.SubjectId = id;
            return HandleResult(await mediator.Send(command));
        }

        /// <summary>
        /// Xóa một môn học khỏi hệ thống
        /// </summary>
        /// <remarks>
        /// Yêu cầu Policy: **AdminOnly**.
        /// </remarks>
        /// <param name="id">Mã môn học cần xóa</param>
        /// <response code="200">Xóa môn học thành công</response>
        /// <response code="401">Chưa xác thực JWT</response>
        /// <response code="403">Không đủ quyền truy cập (Chỉ Admin)</response>
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        [SwaggerOperation(Summary = "Xóa môn học", Description = "Xóa vĩnh viễn môn học khỏi hệ thống (Chỉ Admin)")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Delete(string id) => Ok(await mediator.Send(new DeleteSubjectCommand(id)));
    }
}

