namespace SpareParts.Domain
{
    public class ChangeCountShoppingCartDto
    {
        public Guid ProductId { get; set; }

        public int Count { get; set; }

        public Guid? UserId { get; set; }
    }
}