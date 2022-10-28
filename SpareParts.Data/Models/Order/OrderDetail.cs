namespace SpareParts.Data
{
    public class OrderDetail : IDbModel<Guid>
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        [ForeignKey("OrderId")] public virtual OrderHeader OrderHeader { get; set; } = new OrderHeader();
        public Guid? ProductId { get; set; }
        [ForeignKey("ProductId")] public virtual Product Product { get; set; } = new Product();
        public int Count { get; set; }
        public double Price { get; set; }
    }
}