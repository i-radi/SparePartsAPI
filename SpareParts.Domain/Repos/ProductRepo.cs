using SpareParts.Data.DbContext;

namespace SpareParts.Domain.Repos;

public class ProductRepo : IDomainRepository<Product>
{
    private readonly ApplicationDbContext _context;

    public ProductRepo(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public IEnumerable<Product> GetAllModel()
        => _context.Products.ToList();

    public Product GetById(int id)
        =>_context.Products.FirstOrDefault(p => p.Id == id)!;

    public void CreateModel(Product model)
    {
        if (model == null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        _context.Products.Add(model);
    }

    public void UpdateModel(Product model)
    {
        if (model == null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        _context.Products.Update(model);
    }

    public void DeleteModel(Product model)
    {
        if (model == null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        _context.Products.Remove(model);
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