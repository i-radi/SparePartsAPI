namespace SpareParts.Domain;

public class LoginDto : IDtos
{
    public LoginDto(string email, string password)
    {
        Email = email;
        Password = password;
    }

    [Required]
    public string Email { get; }

    [Required]
    public string Password { get; }
}
