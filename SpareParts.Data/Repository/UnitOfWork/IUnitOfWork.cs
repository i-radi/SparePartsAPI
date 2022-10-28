

namespace SpareParts.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepo Product { get; }
        IBrandRepo Brand { get; }
        ICategoryRepo Category { get; }
        IReviewRepo Review { get; }
        IShoppingCartRepo ShoppingCart { get; }
        IOrderHeaderRepo OrderHeader { get; }
        IOrderForDetailRepo OrderForDetail { get; }

        int Save();
    }
}