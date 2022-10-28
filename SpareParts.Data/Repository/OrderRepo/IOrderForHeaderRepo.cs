

namespace SpareParts.Data
{
    public interface IOrderHeaderRepo : IRepo<OrderHeader, Guid>
    {
        void UpdateStatus(Guid id, string orderStatus, string paymentStatus );

        void UpdateStripePaymentId(Guid id, string sessionId, string paymentItentId);
    }
}