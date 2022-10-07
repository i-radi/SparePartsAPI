namespace SpareParts.Domain.Dtos.IdentityDtos
{
    public class ChangePasswordDto : IDto
    {
        public ChangePasswordDto(Guid id, string password, string newPassword)
        {
            Id = id;
            Password = password;
            NewPassword = newPassword;
        }

        public Guid Id { get; set; }

        [Required, StringLength(256)]
        public string Password { get; }

        [Required, StringLength(256)]
        public string NewPassword { get; }
    }
}