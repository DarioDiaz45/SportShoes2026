using SportShoes2026.Entities;
using System.Drawing;

namespace SportShoes2026.Data.Interfaces
{
    public interface ISizeRepository
    {
        List<SiZe> GetAll();

        SiZe? GetById(int id);

        IQueryable<SiZe> Query();

        void Update(SiZe size);

        bool ExistSameNumber(decimal number,int? sizeId = null);
    }
}

