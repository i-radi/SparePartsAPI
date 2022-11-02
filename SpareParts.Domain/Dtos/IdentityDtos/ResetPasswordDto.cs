namespace SpareParts.Domain
{
    public class ResetPasswordDto : IDtos
    {
        public ResetPasswordDto(string email, string otp, string newPassword)
        {
            Email = email;
            OTP = otp;
            NewPassword = newPassword;
        }
        
        public string Email { get; set; }

        [Required, StringLength(10)]
        // ReSharper disable once InconsistentNaming
        public string OTP { get; set; }

        [Required, StringLength(256)]
        public string NewPassword { get; set; }
    }
}