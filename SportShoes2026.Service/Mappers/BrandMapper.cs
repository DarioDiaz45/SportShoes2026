using SportShoes2026.Entities;
using SportShoes2026.Service.DTOs.Brand;

namespace SportShoes2026.Service.Mappers
{
    public static class BrandMapper
    {
        public static Brand ToEntity(BrandCreateDto dto)
        {
            return new Brand
            {
                BrandName = dto.BrandName
            };
        }

        public static BrandListDto ToListDto(Brand brand)
        {
            return new BrandListDto
            {
                BrandId = brand.BrandId,
                BrandName = brand.BrandName
            };
        }

        public static BrandUpdateDto ToUpdateDto(Brand brand)
        {
            return new BrandUpdateDto
            {
                BrandId = brand.BrandId,
                BrandName = brand.BrandName
            };
        }
    }
}
