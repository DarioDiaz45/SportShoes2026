using SportShoes2026.Service.Common;
using SportShoes2026.Service.DTOs.Brand;

namespace SportShoes2026.Service.Interfaces
{
    public interface IBrandService
    {
        Result<List<BrandListDto>> GetAll();

        Result Add(BrandCreateDto dto);

        Result Delete(int id);

        Result<BrandUpdateDto> GetForUpdate(int id);

        Result Update(BrandUpdateDto dto);
    }
}
