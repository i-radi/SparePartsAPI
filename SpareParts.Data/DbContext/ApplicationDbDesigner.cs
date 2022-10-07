using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;

namespace SpareParts.Data.DbContext;

public class ApplicationDbDesigner : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public static DbContextOptionsBuilder SetConfiguration<TContext>(IServiceProvider provider,
        DbContextOptionsBuilder builder, string connectionString)
        where TContext : Microsoft.EntityFrameworkCore.DbContext
    {
        if (builder is DbContextOptionsBuilder<TContext> gBuilder)
            return SetConfiguration(gBuilder, connectionString, provider);
        return builder;
    }

    public static DbContextOptionsBuilder<TContext> SetConfiguration<TContext>(
        DbContextOptionsBuilder<TContext> builder, string connectionString, IServiceProvider provider = null)
        where TContext : Microsoft.EntityFrameworkCore.DbContext
    {
        builder.UseSqlServer(connectionString, options =>
        {
            options.EnableRetryOnFailure(3);
            options.CommandTimeout(45);
        });
#if DEBUG
        builder.EnableSensitiveDataLogging()
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableDetailedErrors();
#endif
        return builder;
    }


    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = SetConfiguration(new DbContextOptionsBuilder<ApplicationDbContext>(),
            "Data Source=.;Initial Catalog=SparePartsDB;Integrated Security=True;");
        return new ApplicationDbContext(optionsBuilder.Options);
    }
}