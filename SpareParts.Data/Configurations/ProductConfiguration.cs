using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpareParts.Data.Models;
using System.Text.Json;

namespace SpareParts.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder
            .Property(b => b.Name)
            .HasMaxLength(400)
            .IsRequired();
        //builder
        //    .HasData(SeedData.GetData());
    }
}

//public static class SeedData
//{
//    public static List<Product> GetData()
//    {
//        string fileName = @"../SpareParts.Data/SeedData/Spare Parts Metadata.json";
//        string jsonString = File.ReadAllText(fileName);
        
//        var products = JsonSerializer.Deserialize<List<Product>>(jsonString);
//        return products;
//    }
//}