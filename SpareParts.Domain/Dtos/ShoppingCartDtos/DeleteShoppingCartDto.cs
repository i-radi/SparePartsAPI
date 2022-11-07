namespace SpareParts.Domain
{
    public class DeleteShoppingCartDto
    {
        public Guid ProductId { get; set; }

        public Guid? UserId { get; set; }
    }
}