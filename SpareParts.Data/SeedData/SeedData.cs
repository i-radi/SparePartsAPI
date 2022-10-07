using SpareParts.Data.DbContext;
using SpareParts.Data.Models;
using SpareParts.Data.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SpareParts.Data.SeedData
{
    public static class SeedData
    {
        public static void jsonToDb(ApplicationDbContext _context)
        {
            using var context = _context;
            context.Database.EnsureCreated();
            var product = context.Products.FirstOrDefault(b => b.Id == 0);
            string fileName = @"../../../../SeedData/Spare Parts Metadata.json";
            string jsonString = File.ReadAllText(fileName);

            var products = JsonSerializer.Deserialize<List<Product>>(jsonString);

            if (product is null && products is not null)
            {
                context.Products.AddRange(products);
            }

            context.SaveChanges();
        }
    }
}
