using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SpareParts.Data.Configurations;
using SpareParts.Data.Models;

namespace SpareParts.Data.DbContext;

public class ApplicationDbContext: IdentityDbContext<User, IdentityRole<Guid>, Guid, IdentityUserClaim<Guid>, IdentityUserRole<Guid>, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Address> Addresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        var typesToRegister = GetType().Assembly
            .GetTypes()
            .Where(type => type.GetInterfaces()
                .Any(i => i.IsGenericType &&
                          i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));
        foreach (Type type in typesToRegister)
        {
            dynamic configurationInstance = Activator.CreateInstance(type)!;
            if (configurationInstance != null)
                modelBuilder.ApplyConfiguration(configurationInstance);
        }

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AddressConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderItemConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
        //modelBuilder.Entity<SparePart>();
        //modelBuilder.Ignore<SparePart>();
        //new UserConfiguration().Configure(modelBuilder.Entity<User>());
        //modelBuilder.Entity<SparePart>().ToTable("SpareParts",b => b.ExcludeFromMigrations());

        //Change Identity Schema and Table Names
        modelBuilder.Entity<IdentityRole<Guid>>().ToTable("Roles", "User");
        modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles", "User");
        modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims", "User");
        modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins", "User");
        modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims", "User");
        modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens", "User");


    }
}