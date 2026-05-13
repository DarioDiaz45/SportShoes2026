using Microsoft.EntityFrameworkCore;
using SportShoes2026.Data.Interfaces;
using SportShoes2026.Entities;

namespace SportShoes2026.Data.Repositories
{
    public class SportShoeRepository:ISportShoeRepository
    {
        private readonly ShoesDbContext
           _context;

        public SportShoeRepository(ShoesDbContext context)
        {
            _context = context;
        }

        public void Add(SportShoe shoe)
        {
            _context.SportShoes.Add(shoe);
        }

        public void Delete(int id)
        {
            var shoe =
                _context.SportShoes.Find(id);

            if (shoe != null)
            {
                shoe.Active = false;
            }
        }

        public bool ExistSameSportShoe(
            string model,
            int brandId,
            int sizeId,
            int? sportShoeId = null)
        {
            return _context.SportShoes.Any(s =>
                s.Model == model &&
                s.BrandId == brandId &&
                s.GenreId == sizeId &&
                (sportShoeId == null ||
                 s.ShoeId != sportShoeId));
        }

        public List<SportShoe> GetAll()
        {
            return _context.SportShoes
            .Include(s => s.Brand)
            .Include(s => s.Genre)
            .Include(s => s.Sport)
            .Include(s => s.Size)
            .Where(s => s.Active)
            .AsNoTracking()
            .ToList();
        }

        public SportShoe? GetById(int id)
        {
            return _context.SportShoes
                .Include(s => s.Brand)
                .Include(s => s.Genre)
                .Include(s => s.Sport)
                .FirstOrDefault(
                    s => s.ShoeId == id);
        }

        public IQueryable<SportShoe> Query()
        {
            return _context.SportShoes
                .Include(s => s.Brand)
                .Include(s => s.Genre)
                .Include(s => s.Sport)
                .AsNoTracking()
                .AsQueryable();
        }

        public void Update(SportShoe shoe)
        {
            _context.SportShoes.Update(shoe);
        }
    }
}
