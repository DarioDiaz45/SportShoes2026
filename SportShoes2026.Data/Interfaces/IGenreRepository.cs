using SportShoes2026.Entities;

namespace SportShoes2026.Data.Interfaces
{
    public interface IGenreRepository
    {
        IEnumerable<Genre> GetAll();
    }
}
