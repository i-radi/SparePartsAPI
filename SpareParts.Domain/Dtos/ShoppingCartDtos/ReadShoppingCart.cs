namespace SpareParts.Domain
{
    public class ReadShoppingCartDto
    {
        public string ProductName { get; set; } = String.Empty;
        public string ImgPath { get; set; } = String.Empty;
        public string ModelNumber { get; set; } = String.Empty;
        public double Price { get; set; }
        public int Count { get; set; }
        public Guid? ProductId { get; set; }
        public Guid? UserId { get; set; }
    }
}