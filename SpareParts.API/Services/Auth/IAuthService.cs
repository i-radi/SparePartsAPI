namespace SpareParts.API.Services.Auth
{
    public interface IAuthService
    {
        Task<AuthDto> ChangePasswordAsync(ChangePasswordDto dto);
        Task SendPasswordResetCodeAsync(string email);
        Task<IdentityResult> ResetPasswordAsync(ResetPasswordDto dto);
        Task<AuthDto> RegisterAsync(RegisterDto dto);
        Task<AuthDto> LoginAsync(LoginDto dto);
        Task<AuthDto> UpdateUserAsync(UpdateUserDto dto);
    }
}