

namespace SpareParts.Data
{
    public class OrderHeaderRepo : Repo<OrderHeader, Guid>, IOrderHeaderRepo
    {
        private readonly ApplicationDbContext _context;

        public OrderHeaderRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void UpdateStatus(Guid id, string orderStatus, string paymentStatus )
        {
            var orderFromDb = _context.OrderHeaders.FirstOrDefault(u => u.Id == id);
            if (orderFromDb != null)
            {
                orderFromDb.OrderStatus = orderStatus;
                if (paymentStatus != null)
                {
                    orderFromDb.PaymentStatus = paymentStatus;
                }
            }
        }

        public void UpdateStripePaymentId(Guid id, string sessionId, string paymentItentId)
        {
            var orderFromDb = _context.OrderHeaders.FirstOrDefault(u => u.Id == id);
            orderFromDb!.PaymentDate = DateTime.Now;
            orderFromDb.SessionId = sessionId;
            orderFromDb.PaymentIntentId = paymentItentId;
        }
    }
}