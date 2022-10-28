namespace SpareParts.Domain
{
    public class RegisterDto : IDtos
    {
        public RegisterDto(
            string name,
            string userName,
            string email,
            string password,
            string phoneNumber,
            string role
            )
        {
            Name = name;
            UserName = userName;
            Email = email;
            Password = password;
            PhoneNumber = phoneNumber;
            Role = role;
        }

        [StringLength(100)]
        public string Name { get; set; }

        [Required, StringLength(50)]
        public string UserName { get; set; }

        [Required, StringLength(128)]
        public string Email { get; set; }

        [Required, StringLength(50)]
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set;}
    }
}