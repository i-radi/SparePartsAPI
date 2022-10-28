
namespace SpareParts.Data
{
    public sealed class Brand:IDbModel<int>
    {
        public Brand()
        {
            Products = new HashSet<Product>();
        }
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ImgPath { get; set; } = string.Empty;
        public ICollection<Product> Products { get; set; }
    }
}