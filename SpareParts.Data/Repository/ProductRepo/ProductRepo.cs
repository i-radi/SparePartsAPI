
namespace SpareParts.Data
{
    public class ProductRepo : Repo<Product, Guid>, IProductRepo
    {
        //private readonly ApplicationDbContext _context;

        public ProductRepo(ApplicationDbContext context) : base(context)
        {
            //_context = context;
        }
    }
}