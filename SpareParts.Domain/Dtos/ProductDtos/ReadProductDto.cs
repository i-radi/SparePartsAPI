namespace SpareParts.Domain;

    public class ReadProductDto:ReadDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Manufacture { get; set; } = String.Empty;
        public string ModelNumber { get; set; } = String.Empty;
        public string Details { get; set; } = String.Empty;
        public string ImgPath { get; set; } = String.Empty;
        public double Price { get; set; } = 1;
        public int Count { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
}
