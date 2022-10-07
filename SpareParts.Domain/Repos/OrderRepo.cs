using SpareParts.Data.DbContext;

namespace SpareParts.Domain.Repos;

public class OrderRepo : IDomainRepository<Order>
{
    private readonly ApplicationDbContext _context;

    public OrderRepo(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public IEnumerable<Order> GetAllModel()
        => _context.Orders.ToList();

    public Order GetById(int id)
        =>_context.Orders.FirstOrDefault(p => p.Id == id)!;

    public void CreateModel(Order model)
    {
        if (model == null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        _context.Orders.Add(model);
    }

    public void UpdateModel(Order model)
    {
        if (model == null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        _context.Orders.Update(model);
    }

    public void DeleteModel(Order model)
    {
        if (model == null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        _context.Orders.Remove(model);
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