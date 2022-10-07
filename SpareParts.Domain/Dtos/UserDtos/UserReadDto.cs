namespace SpareParts.Domain.Dtos.UserDtos;

public class UserReadDto : ReadDto<User>
{
    public Guid Id { get; set; }
    public UserType UserType { get; set; }
    public string UserName { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}