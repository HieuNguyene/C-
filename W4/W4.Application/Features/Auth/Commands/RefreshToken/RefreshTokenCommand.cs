using System.Security.Claims;
using MediatR;
using Microsoft.IdentityModel.JsonWebTokens;
using W4.Application.DTOs;
using W4.Application.DTOs.Responses;
using W4.Application.Interfaces;

namespace W4.Application.Features.Auth.Commands.RefreshToken
{
    public class RefreshTokenCommand : IRequest<ApiResponse<LoginResponse>>
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }

    public class RefreshTokenCommandHandler(IUserRepository userRepo, ITokenService tokenService)
        : IRequestHandler<RefreshTokenCommand, ApiResponse<LoginResponse>>
    {
        public async Task<ApiResponse<LoginResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            // 1. Giải mã Access Token cũ
            var principal = tokenService.GetClaimsPrincipalFromExpiredToken(request.AccessToken);
            if (principal == null)
            {
                return new ApiResponse<LoginResponse> { Success = false, Message = "Access Token không hợp lệ!" };
            }

            var jwtId = principal.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;
            var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? principal.FindFirst("sub")?.Value;
            var username = principal.FindFirst(ClaimTypes.Name)?.Value ?? principal.FindFirst("unique_name")?.Value;
            var role = principal.FindFirst(ClaimTypes.Role)?.Value ?? "User";

            // 2. Tìm Refresh Token trong Database qua Repository
            var storedToken = await userRepo.GetRefreshTokenAsync(request.RefreshToken);
            if (storedToken == null)
            {
                return new ApiResponse<LoginResponse> { Success = false, Message = "Refresh Token không tồn tại!" };
            }

            // 3. Kiểm tra các điều kiện an toàn
            if (storedToken.IsUsed)
            {
                return new ApiResponse<LoginResponse> { Success = false, Message = "Refresh Token này đã từng được sử dụng rồi!" };
            }
            if (storedToken.IsRevoked)
            {
                return new ApiResponse<LoginResponse> { Success = false, Message = "Refresh Token đã bị thu hồi!" };
            }
            if (storedToken.JwtId != jwtId)
            {
                return new ApiResponse<LoginResponse> { Success = false, Message = "Token không khớp cặp!" };
            }
            if (storedToken.ExpiryDate < DateTime.UtcNow)
            {
                return new ApiResponse<LoginResponse> { Success = false, Message = "Refresh Token đã hết hạn!" };
            }

            // 4. Đánh dấu token cũ ĐÃ DÙNG
            storedToken.IsUsed = true;
            await userRepo.UpdateRefreshTokenAsync(storedToken);

            // 5. Cấp CẶP TOKEN MỚI
            var newAccessToken = tokenService.GenerateToken(userId!, username!, role, out string newJwtId);
            var newRefreshToken = tokenService.GenerateRefreshToken();

            var newRefreshTokenEntity = new W4.Domain.Entities.RefreshToken
            {
                Id = Guid.NewGuid(),
                Token = newRefreshToken,
                JwtId = newJwtId,
                UserId = userId!,
                CreatedAt = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddDays(7),
                IsUsed = false,
                IsRevoked = false
            };
            await userRepo.SaveRefreshTokenAsync(newRefreshTokenEntity);

            return new ApiResponse<LoginResponse>
            {
                Success = true,
                Message = "Làm mới Token thành công!",
                Data = new LoginResponse
                {
                    Token = newAccessToken,
                    RefreshToken = newRefreshToken,
                    Username = username!,
                    Role = role
                }
            };
        }
    }
}