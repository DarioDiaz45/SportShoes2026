namespace SportShoes2026.Entities
{
    public class Sport
    {
        public int SportId { get; set; }
        public string SportName { get; set; } = null!;
        public bool Active { get; set; }
        public ICollection<SportShoe> SportShoes { get; set; } = new List<SportShoe>();
    }
}
