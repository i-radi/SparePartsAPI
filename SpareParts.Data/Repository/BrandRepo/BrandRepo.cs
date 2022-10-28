

namespace SpareParts.Data
{
    public class BrandRepo : Repo<Brand, int>, IBrandRepo
    {
        //private readonly ApplicationDbContext _context;

        public BrandRepo(ApplicationDbContext context) : base(context)
        {
            //_context = context;
        }
    }
}