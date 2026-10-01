using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using W4.Application.DTOs;
using W4.Application.DTOs.Responses;
using W4.Application.Features.Auth.Commands.Login;
using W4.Application.Features.Auth.Commands.RefreshToken;
using W4.Application.Features.Auth.Commands.Register;
using W4.Application.Features.Auth.Commands.RevokeToken;

namespace W4.API.Controllers
{
    [AllowAnonymous]
    [Route("api/auth")]
    [ApiController]
    [SwaggerTag("Xác thực & Phân quyền (Authentication & Authorization)")]
    public class AuthController(IMediator mediator) : ApiControllerBase
    {
        /// <summary>
        /// Đăng ký tài khoản người dùng mới
        /// </summary>
        /// <remarks>
        /// Tạo một tài khoản mới với vai trò được chỉ định (mặc định là 'User', hoặc 'Admin').
        /// Mật khẩu được mã hóa an toàn bằng thuật toán PBKDF2.
        /// </remarks>
        /// <param name="command">Thông tin đăng ký bao gồm Username, Password, Email, Role</param>
        /// <response code="200">Đăng ký thành công, trả về Id của tài khoản vừa tạo</response>
        /// <response code="400">Tên người dùng đã tồn tại hoặc dữ liệu không hợp lệ</response>
        [HttpPost("register")]
        [SwaggerOperation(Summary = "Đăng ký tài khoản", Description = "Tạo tài khoản mới và lưu thông tin vào hệ thống")]
        [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterCommand command)
        {
            var result = await mediator.Send(command);
            return HandleResult(result);
        }

        /// <summary>
        /// Đăng nhập hệ thống để nhận Access Token và Refresh Token
        /// </summary>
        /// <remarks>
        /// Khi đăng nhập thành công, hệ thống cấp phát:
        /// - **AccessToken:** Token định danh JWT (có thời hạn 15 phút).
        /// - **RefreshToken:** Token xoay vòng (lưu trong DB) dùng để lấy AccessToken mới mà không cần đăng nhập lại.
        /// </remarks>
        /// <param name="command">Thông tin tên đăng nhập và mật khẩu</param>
        /// <response code="200">Đăng nhập thành công, trả về AccessToken và RefreshToken</response>
        /// <response code="400">Sai tên đăng nhập hoặc mật khẩu</response>
        [HttpPost("login")]
        [SwaggerOperation(Summary = "Đăng nhập hệ thống", Description = "Xác thực tài khoản và trả về cặp Token JWT")]
        [ProducesResponseType(typeof(ApiResponse<LoginResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<LoginResponse>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> LoginAsync([FromBody] LoginCommand command)
        {
            var result = await mediator.Send(command);
            return HandleResult(result);
        }

        /// <summary>
        /// Làm mới Access Token bằng Refresh Token (Token Rotation)
        /// </summary>
        /// <remarks>
        /// Khi Access Token hết hạn, gửi Access Token cũ kèm Refresh Token để nhận cặp Token mới.
        /// Cơ chế **Refresh Token Rotation** sẽ hủy token cũ và phát sinh token mới để chống đánh cắp.
        /// </remarks>
        /// <param name="command">Cặp AccessToken cũ và RefreshToken hiện tại</param>
        /// <response code="200">Cấp mới Token thành công</response>
        /// <response code="400">Refresh Token không hợp lệ hoặc đã hết hạn/bị thu hồi</response>
        [HttpPost("refresh-token")]
        [SwaggerOperation(Summary = "Làm mới Access Token", Description = "Xoay vòng token và cấp phát Access Token mới")]
        [ProducesResponseType(typeof(ApiResponse<LoginResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<LoginResponse>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenCommand command)
        {
            var result = await mediator.Send(command);
            return HandleResult(result);
        }

        /// <summary>
        /// Thu hồi Refresh Token (Đăng xuất khỏi thiết bị)
        /// </summary>
        /// <remarks>
        /// Vô hiệu hóa một Refresh Token, khiến nó không thể sử dụng để lấy Access Token mới nữa.
        /// </remarks>
        /// <param name="command">Chuỗi Refresh Token cần thu hồi</param>
        /// <response code="200">Thu hồi token thành công</response>
        /// <response code="400">Refresh Token không tồn tại</response>
        [HttpPost("revoke-token")]
        [SwaggerOperation(Summary = "Thu hồi Refresh Token", Description = "Vô hiệu hóa Refresh Token (Đăng xuất)")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RevokeTokenAsync([FromBody] RevokeTokenCommand command)
        {
            var result = await mediator.Send(command);
            return HandleResult(result);
        }
    }
}