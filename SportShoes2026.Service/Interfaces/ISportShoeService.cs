using SportShoes2026.Entities;
using SportShoes2026.Service.Common;
using SportShoes2026.Service.DTOs.SportShoe;

namespace SportShoes2026.Service.Interfaces
{
    public interface ISportShoeService
    {
        Result<List<SportShoeListDto>>
            GetAll();

        Result<SportShoeDetailsDto>
            GetDetails(int id);

        Result<SportShoeUpdateDto>
            GetForUpdate(int id);

        Result Add(
            SportShoeCreateDto dto);

        Result Update(
            SportShoeUpdateDto dto);

        Result Delete(int id);
        Result<List<Genre>> GetGenres();
    }
}
