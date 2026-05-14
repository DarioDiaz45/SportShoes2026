using SportShoes2026.Entities;

namespace SportShoes2026.Data.Interfaces
{
    public interface ISportShoeRepository
    {
        List<SportShoe> GetAll();

        IQueryable<SportShoe> Query();

        SportShoe? GetById(int id);

        void Add(SportShoe shoe);

        void Update(SportShoe shoe);

        void Delete(int id);

        bool ExistSameSportShoe(string model, int brandId, int sizeId, int? sportShoeId = null);
    }
}
