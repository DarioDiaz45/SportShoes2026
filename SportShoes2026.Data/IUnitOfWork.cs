

using SportShoes2026.Data.Interfaces;

namespace SportShoes2026.Data
{
    public interface IUnitOfWork
    {
        IBrandRepository Brands { get; }

        ISportRepository Sports { get; }

        ISizeRepository Sizes { get; }

        ISportShoeRepository SportShoes { get; }

        IGenreRepository Genres { get; }

        void Save();
    }
}
