

namespace SpareParts.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IProductRepo Product { get; private set; }
        public IBrandRepo Brand { get; private set; }
        public ICategoryRepo Category { get; private set; }
        public IReviewRepo Review { get; private set; }
        public IShoppingCartRepo ShoppingCart { get; private set; }
        public IOrderHeaderRepo OrderHeader { get; private set; }
        public IOrderForDetailRepo OrderForDetail { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Product = new ProductRepo(_context);
            Brand = new BrandRepo(_context);
            Category = new CategoryRepo(_context);
            Review = new ReviewRepo(_context);
            ShoppingCart = new ShoppingCartRepo(_context);
            OrderHeader = new OrderHeaderRepo(_context);
            OrderForDetail = new OrderForDetailRepo(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}