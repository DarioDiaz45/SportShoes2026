namespace SportShoes2026.Service.DTOs.SportShoe
{
    public class SportShoeUpdateDto
    {
        public int SportShoeId { get; set; }

        public string Model { get; set; } = null!;

        public decimal Price { get; set; }

        public DateTime ReleaseDate { get; set; }

        public int BrandId { get; set; }

        public int SportId { get; set; }

        public int GenreId { get; set; }
    }
}
