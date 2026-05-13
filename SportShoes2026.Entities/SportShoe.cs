using System.Drawing;

namespace SportShoes2026.Entities
{
    public class SportShoe
    {
        public int ShoeId { get; set; }

        public string Model { get; set; } = null!;

        
        public string Description { get; set; } = null!;

        public decimal Price { get; set; }

        public bool Active { get; set; } = true;

        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; } = null!;

        public int SportId { get; set; }
        public virtual Sport Sport { get; set; } = null!;

       
        public int GenreId { get; set; }
        public virtual Genre Genre { get; set; } = null!;

        public int SizeId { get; set; }
        public virtual SiZe Size { get; set; } = null!;
    }
}
