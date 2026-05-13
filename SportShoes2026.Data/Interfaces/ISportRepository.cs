using SportShoes2026.Entities;

namespace SportShoes2026.Data.Interfaces
{
    public interface ISportRepository
    {
        List<Sport> GetAll();

        Sport? GetById(int id);

        IQueryable<Sport> Query();

        void Add(Sport sport);

        void Update(Sport sport);

        void Delete(int id);

        bool ExistSameName(string name, int? sportId = null);

        bool HasSportShoes(int id);
    }
}
