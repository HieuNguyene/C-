using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using W4.Application.DTOs;
using W4.Application.DTOs.Responses;
using W4.Application.Features.Classes.Commands;
using W4.Application.Features.Classes.Queries;

namespace W4.API.Controllers
{
    [Authorize]
    [Route("api/class")]
    [ApiController]
    [SwaggerTag("Quản lý lớp học (Class Management)")]
    public class ClassController(IMediator mediator) : ApiControllerBase
    {
        /// <summary>
        /// Lấy danh sách tất cả các lớp học
        /// </summary>
        /// <remarks>
        /// Dữ liệu được hỗ trợ bởi **Distributed Cache (Redis)** để tăng tốc độ truy xuất.
        /// Bất kỳ người dùng nào đã đăng nhập đều có thể truy cập.
        /// </remarks>
        /// <response code="200">Lấy danh sách lớp thành công</response>
        /// <response code="401">Chưa xác thực JWT Bearer</response>
        [HttpGet]
        [SwaggerOperation(Summary = "Danh sách lớp học", Description = "Lấy tất cả các lớp học hiện có trong hệ thống (sử dụng Redis Cache)")]
        [ProducesResponseType(typeof(ApiResponse<List<ClassResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAll() => Ok(await mediator.Send(new GetAllClassesQuery()));

        /// <summary>
        /// Lấy thông tin chi tiết của một lớp học theo mã lớp
        /// </summary>
        /// <remarks>
        /// Bất kỳ người dùng nào đã đăng nhập đều có thể truy cập.
        /// </remarks>
        /// <param name="id">Mã lớp học (ví dụ: '10A1')</param>
        /// <response code="200">Tìm thấy lớp học</response>
        /// <response code="401">Chưa xác thực JWT Bearer</response>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Chi tiết lớp học", Description = "Lấy thông tin một lớp học theo mã lớp")]
        [ProducesResponseType(typeof(ApiResponse<ClassResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetById(string id) => Ok(await mediator.Send(new GetClassByIdQuery(id)));

        /// <summary>
        /// Tạo mới một lớp học
        /// </summary>
        /// <remarks>
        /// Yêu cầu Policy: **AdminOnly**. Khi tạo thành công, cache Redis sẽ được tự động xóa/cập nhật.
        /// </remarks>
        /// <param name="command">Thông tin mã lớp và tên lớp học</param>
        /// <response code="200">Tạo lớp học thành công</response>
        /// <response code="401">Chưa xác thực JWT</response>
        /// <response code="403">Không đủ quyền (Chỉ dành cho Admin)</response>
        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        [SwaggerOperation(Summary = "Tạo lớp học mới", Description = "Thêm một lớp học mới vào cơ sở dữ liệu (Chỉ Admin)")]
        [ProducesResponseType(typeof(ApiResponse<ClassResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Create([FromBody] CreateClassCommand command) => Ok(await mediator.Send(command));

        /// <summary>
        /// Cập nhật thông tin một lớp học
        /// </summary>
        /// <remarks>
        /// Yêu cầu Policy: **AdminOnly**.
        /// </remarks>
        /// <param name="id">Mã lớp học cần cập nhật</param>
        /// <param name="command">Dữ liệu tên lớp học mới</param>
        /// <response code="200">Cập nhật thành công</response>
        /// <response code="400">Dữ liệu không hợp lệ hoặc lớp không tồn tại</response>
        /// <response code="401">Chưa xác thực JWT</response>
        /// <response code="403">Không đủ quyền (Chỉ dành cho Admin)</response>
        [HttpPut("{id}")]
        [Authorize(Policy = "AdminOnly")]
        [SwaggerOperation(Summary = "Cập nhật lớp học", Description = "Sửa đổi thông tin lớp học (Chỉ Admin)")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateClassCommand command)
        {
            command.ClassId = id;
            return HandleResult(await mediator.Send(command));
        }

        /// <summary>
        /// Xóa một lớp học khỏi hệ thống
        /// </summary>
        /// <remarks>
        /// Yêu cầu Policy: **AdminOnly**.
        /// </remarks>
        /// <param name="id">Mã lớp học cần xóa</param>
        /// <param name="command">Command xóa lớp học</param>
        /// <response code="200">Xóa lớp học thành công</response>
        /// <response code="400">Lớp học không tồn tại hoặc có ràng buộc dữ liệu</response>
        /// <response code="401">Chưa xác thực JWT</response>
        /// <response code="403">Không đủ quyền (Chỉ dành cho Admin)</response>
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        [SwaggerOperation(Summary = "Xóa lớp học", Description = "Xóa lớp học theo mã lớp (Chỉ Admin)")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Delete(string id, [FromBody] DeleteClassCommand command)
        {
            command.ClassId = id;
            return HandleResult(await mediator.Send(command));
        }
    }
}

