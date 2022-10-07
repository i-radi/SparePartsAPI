namespace SpareParts.Domain.Dtos.IdentityDtos
{
    public class RegisterDto : IDto
    {
        public RegisterDto(
            string name,
            string userName,
            string email,
            string password,
            UserType userType,
            string phoneNumber
            )
        {
            Name = name;
            UserName = userName;
            Email = email;
            Password = password;
            UserType = userType;
            PhoneNumber = phoneNumber; ;
        }
        

        [Required, StringLength(50)]
        public string Name { get; }

        [Required, StringLength(50)]
        public string UserName { get; }

        [Required, StringLength(128)]
        public string Email { get; }

        [Required, StringLength(50)]
        public string Password { get; }
        public string PhoneNumber { get; }
        public UserType UserType { get; }
    }
}