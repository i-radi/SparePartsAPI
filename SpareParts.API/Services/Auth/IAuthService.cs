using SpareParts.Domain.Dtos.IdentityDtos;
using SpareParts.Domain.Dtos.Jwt;

namespace SpareParts.API.Services.Auth
{
    public interface IAuthService
    {
        Task<AuthDto> ChangePasswordAsync(ChangePasswordDto dto);
        Task<AuthDto> RegisterAsync(RegisterDto dto);
        Task<AuthDto> GetTokenAsync(TokenRequestDto dto);
        Task<string> AddRoleAsync(AddRoleDto dto);
    }
}