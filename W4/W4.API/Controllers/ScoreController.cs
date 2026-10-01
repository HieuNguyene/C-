using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using W4.Application.DTOs;
using W4.Application.DTOs.Responses;
using W4.Application.Features.Scores.Commands;
using W4.Application.Features.Scores.Queries;

namespace W4.API.Controllers
{
    [Authorize]
    [Route("api/score")]
    [ApiController]
    [SwaggerTag("Quản lý điểm số (Score Management)")]
    public class ScoreController(IMediator mediator) : ApiControllerBase
    {
        /// <summary>
        /// Xem thông tin điểm số theo ID
        /// </summary>
        /// <remarks>
        /// Bất kỳ người dùng đã đăng nhập đều có thể tra cứu điểm.
        /// </remarks>
        /// <param name="id">Mã ID (Guid) của bản ghi điểm</param>
        /// <response code="200">Tìm thấy bản ghi điểm số</response>
        /// <response code="401">Chưa xác thực JWT Bearer</response>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Xem điểm số theo ID", Description = "Tra cứu chi tiết một bản ghi điểm")]
        [ProducesResponseType(typeof(ApiResponse<ScoreResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetById(Guid id) => Ok(await mediator.Send(new GetScoreByIdQuery(id)));

        /// <summary>
        /// Nhập điểm môn học cho học sinh
        /// </summary>
        /// <remarks>
        /// Yêu cầu Policy: **CanManageStudents** (Admin hoặc Teacher).
        /// Thang điểm thường từ 0 đến 10.
        /// </remarks>
        /// <param name="command">Dữ liệu điểm, mã học sinh (StudentId) và mã môn (SubjectId)</param>
        /// <response code="200">Nhập điểm thành công</response>
        /// <response code="400">Dữ liệu điểm không hợp lệ</response>
        /// <response code="401">Chưa xác thực JWT</response>
        /// <response code="403">Không đủ quyền truy cập (Chỉ Admin và Teacher)</response>
        [HttpPost]
        [Authorize(Policy = "CanManageStudents")]
        [SwaggerOperation(Summary = "Nhập điểm mới", Description = "Thêm bản ghi điểm cho một học sinh (Admin & Teacher)")]
        [ProducesResponseType(typeof(ApiResponse<ScoreResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Create([FromBody] CreateScoreCommand command) => Ok(await mediator.Send(command));

        /// <summary>
        /// Cập nhật điểm số của học sinh
        /// </summary>
        /// <remarks>
        /// Yêu cầu Policy: **CanManageStudents** (Admin hoặc Teacher).
        /// </remarks>
        /// <param name="id">Mã ID của bản ghi điểm cần sửa</param>
        /// <param name="command">Giá trị điểm mới</param>
        /// <response code="200">Cập nhật điểm thành công</response>
        /// <response code="400">Điểm không hợp lệ hoặc không tìm thấy</response>
        /// <response code="401">Chưa xác thực JWT</response>
        /// <response code="403">Không đủ quyền truy cập (Chỉ Admin và Teacher)</response>
        [HttpPut("{id}")]
        [Authorize(Policy = "CanManageStudents")]
        [SwaggerOperation(Summary = "Cập nhật điểm", Description = "Chỉnh sửa điểm số của học sinh (Admin & Teacher)")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateScoreCommand command)
        {
            command.Id = id;
            return HandleResult(await mediator.Send(command));
        }

        /// <summary>
        /// Xóa bản ghi điểm số
        /// </summary>
        /// <remarks>
        /// Yêu cầu Policy: **AdminOnly** (Chỉ Admin mới có quyền xóa điểm số).
        /// </remarks>
        /// <param name="id">Mã ID của bản ghi điểm cần xóa</param>
        /// <response code="200">Xóa điểm thành công</response>
        /// <response code="401">Chưa xác thực JWT</response>
        /// <response code="403">Không đủ quyền truy cập (Chỉ Admin)</response>
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        [SwaggerOperation(Summary = "Xóa điểm số", Description = "Xóa vĩnh viễn một bản ghi điểm (Chỉ Admin)")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Delete(Guid id) => Ok(await mediator.Send(new DeleteScoreCommand(id)));
    }
}

