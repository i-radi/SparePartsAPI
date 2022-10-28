

namespace SpareParts.Data;
    public class CreditCard : IDbModel<Guid>
{
        public Guid Id { get; set; }
        public string Number { get; set; } = String.Empty;
        public string Ccv { get; set; } = String.Empty;
        public Guid UserId { get; set; }
        [ForeignKey("UserId")] public virtual User User { get; set; } = new User();
    }
