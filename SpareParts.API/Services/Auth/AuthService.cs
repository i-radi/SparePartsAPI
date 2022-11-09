using System.Text;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using SpareParts.API.Services.Token;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using SpareParts.InfraStructure;

namespace SpareParts.API.Services.Auth;

public class AuthService : IAuthService
{
    private readonly Jwt _jwt;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly ApplicationDbContext _context;

    public AuthService(
        UserManager<User> userManager,
        ApplicationDbContext context,
        IOptions<Jwt> jwt,
        IMapper mapper)
    {
        _jwt = jwt.Value;
        _mapper = mapper;
        _userManager = userManager;
        _context = context;
    }

    public async Task<AuthDto> LoginAsync(LoginDto dto)
    {
        var authModel = new AuthDto();

        var user = await _userManager.FindByEmailAsync(dto.Email);

        if (user is null || !await _userManager.CheckPasswordAsync(user, dto.Password))
        {
            authModel.Message = "Email or Password is incorrect!";
            return authModel;
        }

        var jwtSecurityToken = await CreateJwtToken(user);

        authModel.Email = user!.Email;
        authModel.IsAuthenticated = true;
        authModel.Username = user.UserName;
        authModel.ExpiresOn = jwtSecurityToken.ValidTo;
        authModel.Role = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)!.Value;
        authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        return authModel;
    }

    public async Task<AuthDto> RegisterAsync(RegisterDto dto)
    {
        if (await _userManager.FindByEmailAsync(dto.Email) is not null)
            return new AuthDto { Message = "Email is already registered!" };

        if (await _userManager.FindByNameAsync(dto.UserName) is not null)
            return new AuthDto { Message = "Username is already registered!" };

        var user = _mapper.Map<User>(dto);

        var result = await _userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded)
        {
            var errors = string.Empty;

            foreach (var error in result.Errors)
                errors += $"{error.Description},";

            return new AuthDto { Message = errors };
        }

        var jwtSecurityToken = await CreateJwtToken(user, dto.Role);

        return new AuthDto
        {
            Email = user.Email,
            ExpiresOn = jwtSecurityToken.ValidTo,
            IsAuthenticated = true,
            Role = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)!.Value,
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            Username = user.UserName
        };
    }

    private async Task<JwtSecurityToken> CreateJwtToken(User user, string role)
    {
        var claims = new[]
            {
                new Claim(ClaimTypes.Role, role==String.Empty?"User":role),
                new Claim(ClaimTypes.Email, user!.Email),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

        await _userManager.AddClaimsAsync(user, claims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            claims: claims,
            issuer: _jwt.Issuer,
            audience: _jwt.Audience,
            signingCredentials: signingCredentials,
            expires: DateTime.Now.AddDays(_jwt.DurationInDays));

        return jwtSecurityToken;
    }

    private async Task<JwtSecurityToken> CreateJwtToken(User user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            claims: userClaims,
            issuer: _jwt.Issuer,
            audience: _jwt.Audience,
            signingCredentials: signingCredentials,
            expires: DateTime.Now.AddDays(_jwt.DurationInDays));

        return jwtSecurityToken;
    }

    public async Task<AuthDto> ChangePasswordAsync(ChangePasswordDto dto)
    {
        var user = await _userManager.FindByIdAsync(dto.Id.ToString());
        var result = await _userManager.ChangePasswordAsync(user, dto.Password, dto.NewPassword);

        if (!result.Succeeded)
        {
            var errors = string.Empty;

            foreach (var error in result.Errors)
                errors += $"{error.Description},";

            return new AuthDto { Message = errors };
        }


        var jwtSecurityToken = await CreateJwtToken(user);

        return new()
        {
            Email = user.Email,
            ExpiresOn = jwtSecurityToken.ValidTo,
            IsAuthenticated = true,
            Role = _userManager.GetRolesAsync(user).Result.FirstOrDefault()!,
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            Username = user.UserName
        };
    }

    public async Task SendPasswordResetCodeAsync(string email)
    {
        // Get Identity User details user user manager
        var user = await _userManager.FindByEmailAsync(email);

        // Generate password reset token
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        // Generate OTP
        int otp = RandomNumberGeneartor.Generate(100000, 999999);

        var resetPassword = new ResetPassword()
        {
            Email = email,
            OTP = otp.ToString(),
            Token = token,
            UserId = user.Id,
            User = user,
            InsertDateTimeUtc = DateTime.UtcNow
        };

        // Save data into db with OTP
        await _context.AddAsync(resetPassword);
        await _context.SaveChangesAsync();

        // to do: Send token in email
        await EmailSender.SendEmailAsync(email, "Reset Password OTP", "Hello "
                                                                      + email + "<br><br>Please find the reset password token below<br><br><b>"
                                                                      + otp + "<b><br><br>Thanks<br>oktests.com");
    }
    
    public async Task<IdentityResult> ResetPasswordAsync(ResetPasswordDto dto)
    {
        // Get Identity User details user user manager
        var user = await _userManager.FindByEmailAsync(dto.Email);

        // getting token from otp
        var resetPasswordDetails = await _context.ResetPasswords
            .Where(rp => rp.OTP == dto.OTP && rp.UserId == user.Id)
            .OrderByDescending(rp => rp.InsertDateTimeUtc)
            .FirstOrDefaultAsync();

        // Verify if token is older than 15 minutes
        var expirationDateTimeUtc = resetPasswordDetails!.InsertDateTimeUtc.AddMinutes(15);

        if (expirationDateTimeUtc < DateTime.UtcNow)
        {
            return null!;
        }

        return await _userManager.ResetPasswordAsync(user, resetPasswordDetails.Token, dto.NewPassword);
    }

    public async Task<AuthDto> UpdateUserAsync(UpdateUserDto dto)
    {
        if (await _userManager.FindByIdAsync(dto.Id.ToString()) is null)
            return new AuthDto { Message = "Invalid User Id!" };

        if (await _userManager.FindByEmailAsync(dto.Email) is not null)
            return new AuthDto { Message = "Email is already registered!" };

        if (await _userManager.FindByNameAsync(dto.Name) is not null)
            return new AuthDto { Message = "Username is already registered!" };

        var user = await _userManager.FindByIdAsync(dto.Id.ToString())!;
        user.Name = dto.Name;
        user.Email = dto.Email;
        user.NormalizedEmail = dto.Email.ToUpper();
        user.PhoneNumber = dto.PhoneNumber;

        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            var errors = string.Empty;

            foreach (var error in result.Errors)
                errors += $"{error.Description},";

            return new AuthDto { Message = errors };
        }

        var jwtSecurityToken = await CreateJwtToken(user);

        return new AuthDto
        {
            Email = user.Email,
            ExpiresOn = jwtSecurityToken.ValidTo,
            IsAuthenticated = true,
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            Username = user.UserName
        };
    }

}