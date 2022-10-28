

namespace SpareParts.Data
{
    public class ReviewRepo : Repo<Review, Guid>, IReviewRepo
    {
        //private readonly ApplicationDbContext _context;

        public ReviewRepo(ApplicationDbContext context) : base(context)
        {
            //_context = context;
        }
    }
}