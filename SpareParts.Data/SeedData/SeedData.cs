

namespace SpareParts.Data
{
    public static class SeedData
    {
        public static void Seed()
        {
            using var context = new ApplicationDbContext();
            context.Database.EnsureCreated();

            //string fileNameBrands = @"../SpareParts.Data/SeedData/Brands.json";
            //string jsonStringBrands = File.ReadAllText(fileNameBrands);
            //var brands = JsonSerializer.Deserialize<List<Brand>>(jsonStringBrands);

            //string fileNameCategories = @"../SpareParts.Data/SeedData/Categories.json";
            //string jsonStringCategories = File.ReadAllText(fileNameCategories);
            //var categories = JsonSerializer.Deserialize<List<Category>>(jsonStringCategories);

            //string fileNameProducts = @"../SpareParts.Data/SeedData/Products.json";
            //string jsonStringProducts = File.ReadAllText(fileNameProducts);
            //var products = JsonSerializer.Deserialize<List<ProductDemo>>(jsonStringProducts);


            //string fileName2 = @"../SpareParts.Data/SeedData/Products2.json";
            //using FileStream createStream2 = File.Create(fileName2);
            //JsonSerializer.Serialize(createStream2, products);
            //createStream2.Dispose();

            //context.Brands.AddRange(brands!);
            //context.Categories.AddRange(categories!);
            //context.SaveChanges();

            //foreach (var product in products!)
            //{
            //    product.UserId = Guid.Parse("<user id>");//change dbcontext to ProductDemo
            //}
            //context.Products.AddRange(products!);
            //context.SaveChanges();
        }
    }
}
