namespace SpareParts.Data
{
    public class ShoppingCart : IDbModel<Guid>
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }=new ();

        [Range(1, 1000, ErrorMessage = "Please enter a value between 1 and 1000")]
        public int Count { get; set; }

        public Guid UserId { get; set; }

        [ForeignKey("UserId")] public virtual User User { get; set; } = new();

        //[NotMapped]
        //public double Price { get; set; }
    }
}