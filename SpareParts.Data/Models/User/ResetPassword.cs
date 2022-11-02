
namespace SpareParts.Data;

public class ResetPassword : IDbModel<int>
{
    [Key]
    public int Id { get; set; }

    [StringLength(450)]
    public Guid UserId { get; set; }

    [StringLength(256)] public string Email { get; set; } = String.Empty;

    [StringLength(5000)]
    public string Token { get; set; } = String.Empty;

    [StringLength(10)]
    // ReSharper disable once InconsistentNaming
    public string OTP { get; set; } = String.Empty;

    public DateTime InsertDateTimeUtc { get; set; }

    [ForeignKey(nameof(UserId))] public User User { get; set; } = new();
}