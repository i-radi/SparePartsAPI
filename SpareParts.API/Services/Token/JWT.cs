namespace SpareParts.API.Services.Token
{
    public class Jwt
    {
        public string Key { get; set; } = String.Empty;
        public string Issuer { get; set; } = String.Empty;
        public string Audience { get; set; } = String.Empty;
        public double DurationInDays { get; set; }
    }
}