namespace SpareParts.Domain
{
    public class AuthDto : IDtos
    {
        public string Message { get; set; }=String.Empty;
        public bool IsAuthenticated { get; set; }
        public string Username { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string Role { get; set; } = String.Empty;
        public string Token { get; set; } = String.Empty;
        public DateTime ExpiresOn { get; set; }
    }
}