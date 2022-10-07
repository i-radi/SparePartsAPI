using SpareParts.Data.DbContext;

namespace SpareParts.Domain.Repos;

public class OrderItemRepo : IDomainRepository<OrderItem>
{
    private readonly ApplicationDbContext _context;

    public OrderItemRepo(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public IEnumerable<OrderItem> GetAllModel()
        => _context.OrderItems.ToList();

    public OrderItem GetById(int id)
        =>_context.OrderItems.FirstOrDefault(p => p.Id == id)!;

    public void CreateModel(OrderItem model)
    {
        if (model == null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        _context.OrderItems.Add(model);
    }

    public void UpdateModel(OrderItem model)
    {
        if (model == null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        _context.OrderItems.Update(model);
    }

    public void DeleteModel(OrderItem model)
    {
        if (model == null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        _context.OrderItems.Remove(model);
    }
    
    public bool SaveChanges()
        => _context.SaveChanges() >= 0;

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _context.Dispose();
        }
    }
}