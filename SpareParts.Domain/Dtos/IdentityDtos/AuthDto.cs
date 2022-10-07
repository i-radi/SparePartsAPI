namespace SpareParts.Domain.Dtos.IdentityDtos
{
    public class AuthDto : IDto
    {
        public string Message { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Username { get; set; }
        public string Email { get; set; } 
        public UserType Role { get; set; }
        //public string Role { get; set; }
        public string Token { get; set; } 
        //public DateTime ExpiresOn { get; set; }
    }
}