namespace SportShoes2026.Service.DTOs.SportShoe
{
    public class SportShoeListDto
    {
        public int SportShoeId { get; set; }

        public string Model { get; set; } = null!;

        public decimal Price { get; set; }

        public string BrandName { get; set; } = null!;

        public decimal SizeNumber { get; set; }

        public string SportName { get; set; } = null!;

        public string GenreName { get; set; } = null!;
    }
}
