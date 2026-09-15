using MediatR;
using W4.Application.DTOs;
using W4.Application.Interfaces;

namespace W4.Application.Features.Auth.Commands.RevokeToken
{
    public class RevokeTokenCommand : IRequest<ApiResponse<bool>>
    {
        public String RefreshToken {get;set;} = string.Empty;
    }
    public class RevokeTokenCommandHandler(IUserRepository repository) : IRequestHandler<RevokeTokenCommand, ApiResponse<bool>>
    {
        public async Task<ApiResponse<bool>> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
        {
            // Tìm refreshtoken 
            var storedToken = await repository.GetRefreshTokenAsync(request.RefreshToken);
            if (storedToken == null)
            {
                return new ApiResponse<bool>
                {
                    Success =false,
                    Message = "Refresh Token không tồn tại",
                    Data = false
                };
            }
            if (storedToken.IsRevoked)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Refresh Token này đã bị thu hồi trước đó rồi!",
                    Data = false
                };
            }
            await repository.RevokeRefreshTokenAsync(storedToken);
            return new ApiResponse<bool>
            {
                Success = true,
                Message ="Thu hồi Token thành công (Đăng xuất)",
                Data = true
            };
        }
    }
}