using Microsoft.EntityFrameworkCore;
using SportShoes2026.Data.Interfaces;
using SportShoes2026.Entities;

namespace SportShoes2026.Data.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly ShoesDbContext _context;
        public BrandRepository(ShoesDbContext context)
        {
            _context = context;
        }

        public void Add(Brand brand)
        {
            _context.Brands.Add(brand);
        }

        public void Delete(int id)
        {
            var brand = _context.Brands.Find(id);

            if (brand != null)
            {
                brand.Active = false;
            }
        }

        public bool ExistSameName(string name, int? brandId = null)
        {
            return _context.Brands.Any(b =>
                b.BrandName == name &&
                (brandId == null || b.BrandId != brandId));
        }

        public List<Brand> GetAll()
        {
            return _context.Brands
                .Where(b => b.Active)
                .AsNoTracking()
                .ToList();
        }

        public Brand? GetById(int id)
        {
            return _context.Brands.Find(id);
        }

        public bool HasSportShoes(int id)
        {
            return _context.SportShoes.Any(s => s.BrandId == id);
        }

        public IQueryable<Brand> Query()
        {
            return _context.Brands
                .AsNoTracking()
                .AsQueryable();
        }

        public void Update(Brand brand)
        {
            _context.Brands.Update(brand);
        }
    }
}
