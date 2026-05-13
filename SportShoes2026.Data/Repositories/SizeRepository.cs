using Microsoft.EntityFrameworkCore;
using SportShoes2026.Data.Interfaces;
using SportShoes2026.Entities;


namespace SportShoes2026.Data.Repositories
{
    public class SizeRepository : ISizeRepository
    {
        private readonly ShoesDbContext _context;
        public SizeRepository(ShoesDbContext context)
        {
            _context = context;
        }

        public bool ExistSameNumber(decimal number, int? sizeId = null)
        {
            return _context.SiZes.Any(s =>
                s.SizeNumber == number &&
                (sizeId == null ||
                 s.SizeId != sizeId));
        }

        public List<SiZe> GetAll()
        {
            return _context.SiZes
               .Where(s => s.Active)
               .AsNoTracking()
               .ToList();
        }

        public SiZe? GetById(int id)
        {
            return _context.SiZes.Find(id);
        }

        public IQueryable<SiZe> Query()
        {
            return _context.SiZes
               .AsNoTracking()
               .AsQueryable();
        }

        public void Update(SiZe size)
        {
            _context.SiZes.Update(size);
        }
    }
}
