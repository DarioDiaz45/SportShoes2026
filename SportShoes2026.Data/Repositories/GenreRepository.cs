using Microsoft.EntityFrameworkCore;
using SportShoes2026.Data.Interfaces;
using SportShoes2026.Entities;

namespace SportShoes2026.Data.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly ShoesDbContext _context;

        public GenreRepository(ShoesDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Genre> GetAll()
        {
            return _context.Genres.AsNoTracking().ToList();
        }
    }
}
