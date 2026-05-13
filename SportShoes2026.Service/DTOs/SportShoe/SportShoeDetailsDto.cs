namespace SportShoes2026.Service.DTOs.SportShoe
{
    public class SportShoeDetailsDto
    {
        public int SportShoeId { get; set; }

        public string Model { get; set; } = null!;

        public decimal Price { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string BrandName { get; set; } = null!;

        public decimal SizeNumber { get; set; }

        public string SportName { get; set; } = null!;
        public int GenreId { get; set; }
        public string GenreName { get; set; } = null!;
    }
}
