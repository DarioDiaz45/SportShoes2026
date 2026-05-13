namespace SportShoes2026.Service.DTOs.SportShoe
{
    public class SportShoeCreateDto
    {
        public string Model { get; set; } = null!;

        public decimal Price { get; set; }

        public DateTime ReleaseDate { get; set; }

        public int BrandId { get; set; }

        public int SizeId { get; set; }

        public int SportId { get; set; }
        public int GenreId { get; set; }

        public string Description { get; set; } = null!;
    }
}
