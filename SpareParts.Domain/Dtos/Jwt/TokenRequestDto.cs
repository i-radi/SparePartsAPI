using System.ComponentModel.DataAnnotations;

namespace SpareParts.Domain.Dtos.Jwt;

public class TokenRequestDto : IDto
{
    public TokenRequestDto(string email, string password)
    {
        Email = email;
        Password = password;
    }

    [Required]
    public string Email { get; }

    [Required]
    public string Password { get; }
}
