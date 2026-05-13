using SportShoes2026.Service.Common;
using SportShoes2026.Service.DTOs.Sport;

namespace SportShoes2026.Service.Interfaces
{
    public interface ISportService
    {
        Result<List<SportListDto>> GetAll();

        Result Add(SportCreateDto dto);

        Result Delete(int id);

        Result<SportUpdateDto> GetForUpdate(int id);

        Result Update(SportUpdateDto dto);
    }
}
