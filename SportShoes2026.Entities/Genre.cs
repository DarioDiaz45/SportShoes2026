namespace SportShoes2026.Entities
{
    public class Genre
    {
        public int GenreId { get; set; }

        public string GenreName { get; set; } = null!;

        public bool Active { get; set; }

        public ICollection<SportShoe> SportShoes { get; set; } = new List<SportShoe>();
    }
}

