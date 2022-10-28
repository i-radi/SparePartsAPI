
namespace SpareParts.Data
{
    public class OrderForDetailRepo : Repo<OrderDetail, Guid>, IOrderForDetailRepo
    {
        //private readonly ApplicationDbContext _context;

        public OrderForDetailRepo(ApplicationDbContext context) : base(context)
        {
            //_context = context;
        }
    }
}