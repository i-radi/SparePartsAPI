

namespace SpareParts.Data
{
    public interface IShoppingCartRepo : IRepo<ShoppingCart, Guid>
    {
        int IncrementCount(ShoppingCart shoppingCart, int count);

        int DecrementCount(ShoppingCart shoppingCart, int count);

        int ChangeCount(ShoppingCart shoppingCart, int count);
    }
}