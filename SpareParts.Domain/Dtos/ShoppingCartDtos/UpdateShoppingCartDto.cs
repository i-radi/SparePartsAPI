namespace SpareParts.Domain
{
    public class UpdateShoppingCartDto
    {
        public Guid ProductId { get; set; }

        public Guid? UserId { get; set; }
    }
}