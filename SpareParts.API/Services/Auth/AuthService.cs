using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SpareParts.API.Helpers;
using SpareParts.Data.DbContext;
using SpareParts.Data.Models;
using SpareParts.Domain.Dtos.IdentityDtos;
using SpareParts.Domain.Dtos.Jwt;
using SpareParts.Domain.Repos;
using SpareParts.InfraStructure.Enums;

namespace SpareParts.API.Services.Auth;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;
    private readonly ApplicationDbContext _context;
    private readonly Jwt _jwt;

    public AuthService(UserManager<User> userManager, IMapper mapper, RoleManager<IdentityRole<Guid>> roleManager, IOptions<Jwt> jwt, ApplicationDbContext context)
    {
        _userManager = userManager;
        _mapper = mapper;
        _roleManager = roleManager;
        _context = context;
        _jwt = jwt.Value;
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
        if (dto.UserType == UserType.Vendor)
        {
            await _userManager.AddToRoleAsync(user, "Vendor");
        }
        else if (dto.UserType == UserType.Client)
        {
            await _userManager.AddToRoleAsync(user, "Client");
        }


        var jwtSecurityToken = await CreateJwtToken(user);

        return new AuthDto
        {
            Email = user.Email,
            //ExpiresOn = jwtSecurityToken.ValidFrom,
            IsAuthenticated = true,
            Role = dto.UserType,
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            Username = user.UserName
        };
    }

    //Login
    public async Task<AuthDto> GetTokenAsync(TokenRequestDto dto)
    {
        var authModel = new AuthDto();

        var user = await _userManager.FindByEmailAsync(dto.Email);

        if (user is null || !await _userManager.CheckPasswordAsync(user, dto.Password))
        {
            authModel.Message = "Email or Password is incorrect!";
            return authModel;
        }

        var jwtSecurityToken = await CreateJwtToken(user);
        var rolesList = await _userManager.GetRolesAsync(user);

        authModel.IsAuthenticated = true;
        authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        authModel.Email = user.Email;
        authModel.Username = user.UserName;
        //authModel.ExpiresOn = jwtSecurityToken.ValidFrom;
        authModel.Role = user.UserType;

        return authModel;
    }

    public async Task<string> AddRoleAsync(AddRoleDto dto)
    {
        var user = await _userManager.FindByIdAsync(dto.UserId);

        if (user is null || !await _roleManager.RoleExistsAsync(dto.Role))
            return "Invalid user ID or Role";

        if (await _userManager.IsInRoleAsync(user, dto.Role))
            return "User already assigned to this role";

        var result = await _userManager.AddToRoleAsync(user, dto.Role);

        return result.Succeeded ? string.Empty : "Something went wrong";
    }

    private async Task<JwtSecurityToken> CreateJwtToken(User user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);
        var roleClaims = new List<Claim>();

        foreach (var role in roles)
            roleClaims.Add(new Claim("roles", role));

        var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id.ToString())
            }
            .Union(userClaims)
            .Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwt.Issuer,
            audience: _jwt.Audience,
            claims: claims,
            expires: DateTime.Now.AddDays(_jwt.DurationInDays),
            signingCredentials: signingCredentials);

        return jwtSecurityToken;
    }

    public async Task<AuthDto> ChangePasswordAsync(ChangePasswordDto dto)
    {
        var user = new UserRepo(_context).GetById(dto.Id);
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
            //ExpiresOn = jwtSecurityToken.ValidFrom,
            IsAuthenticated = true,
            Role = user.UserType,
            //Role = _userManager.GetRolesAsync(user).Result.FirstOrDefault().ToString(),
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            Username = user.UserName
        };
    }
}