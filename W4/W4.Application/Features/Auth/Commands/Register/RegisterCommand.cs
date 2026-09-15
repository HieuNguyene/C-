using MediatR;
using W4.Application.DTOs;
using W4.Application.Interfaces;
using W4.Domain.Entities;

namespace W4.Application.Features.Auth.Commands.Register
{
    public class RegisterCommand : IRequest<ApiResponse<Guid>>
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = "User"; // "Admin" hoặc "User"
    }

    public class RegisterCommandHandler(IUserRepository userRepo, IPasswordHasher passwordHasher) 
        : IRequestHandler<RegisterCommand, ApiResponse<Guid>>
    {
        public async Task<ApiResponse<Guid>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            // 1. Kiểm tra username đã tồn tại chưa
            if (await userRepo.ExistsByUsernameAsync(request.Username))
            {
                return new ApiResponse<Guid> { Success = false, Message = "Tên đăng nhập đã tồn tại!" };
            }

            // 2. Băm mật khẩu (Hash Password)
            var passwordHash = passwordHasher.HashPassword(request.Password);

            // 3. Tạo Entity User và lưu vào Database
            var user = new User(Guid.NewGuid(), request.Username, passwordHash, request.Email, request.Role);
            await userRepo.CreateUserAsync(user);

            return new ApiResponse<Guid>
            {
                Success = true,
                Message = "Đăng ký tài khoản thành công!",
                Data = user.Id
            };
        }
    }
}