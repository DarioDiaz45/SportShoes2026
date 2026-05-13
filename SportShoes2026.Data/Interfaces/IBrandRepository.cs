using SportShoes2026.Entities;

namespace SportShoes2026.Data.Interfaces
{
    public interface IBrandRepository
    {
        List<Brand> GetAll();

        Brand? GetById(int id);

        IQueryable<Brand> Query();

        void Add(Brand brand);

        void Update(Brand brand);

        void Delete(int id);

        bool ExistSameName(string name, int? brandId = null);

        bool HasSportShoes(int id);
    }
}
