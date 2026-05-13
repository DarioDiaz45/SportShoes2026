using SportShoes2026.Service.Common;
using SportShoes2026.Service.DTOs.Size;

namespace SportShoes2026.Service.Interfaces
{
    public interface ISizeService
    {
        Result<List<SizeListDto>> GetAll();

        Result<SizeUpdateDto>GetForUpdate(int id);

        Result Update(SizeUpdateDto dto);
    }
}
