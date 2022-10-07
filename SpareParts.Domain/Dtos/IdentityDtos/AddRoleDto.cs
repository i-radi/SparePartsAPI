namespace SpareParts.Domain.Dtos.IdentityDtos
{
    public class AddRoleDto : IDto
    {
        public AddRoleDto(string userId, string role)
        {
            UserId = userId;
            Role = role;
        }

        [Required]
        public string UserId { get; }

        [Required]//Role Name ,you can use role id
        public string Role { get; }
    }
}