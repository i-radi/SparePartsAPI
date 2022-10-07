using SpareParts.Data.DbContext;
using SpareParts.Data.SeedData;

namespace SpareParts.Domain.Repos;

public class UserRepo 
{
    private readonly ApplicationDbContext _context;

    public UserRepo(ApplicationDbContext context)
    {
        _context = context;
    }
    public void CreateModel(User user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        _context.Users.Add(user);
    }
    public IEnumerable<User> GetAllModel()
        => _context.Users
        .ToList();
    public User GetById(Guid id)
        => _context.Users.SingleOrDefault(u => u.Id == id);
    public void UpdateModel(User user)
    {
        _context.Users.Update(user);
    }
    public void DeleteModel(User user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }
        _context.Users.Remove(user);
    }
    public bool SaveChanges()
        => _context.SaveChanges() >= 0;
    
    #region Dispose
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
    #endregion
}

