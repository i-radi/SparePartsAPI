

namespace SpareParts.Data
{
    public class CategoryRepo : Repo<Category, int>, ICategoryRepo
    {
        //private readonly ApplicationDbContext _context;

        public CategoryRepo(ApplicationDbContext context) : base(context)
        {
            //_context = context;
        }
    }
}