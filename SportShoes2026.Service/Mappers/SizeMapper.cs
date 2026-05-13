using SportShoes2026.Entities;
using SportShoes2026.Service.DTOs.Size;
using System.Drawing;

namespace SportShoes2026.Service.Mappers
{
    public static class SizeMapper
    {
        public static SizeListDto ToListDto(SiZe size)
        {
            return new SizeListDto
            {
                SizeId = size.SizeId,
                Number = size.SizeNumber
            };

        }
        public static SizeUpdateDto ToUpdateDto(SiZe size)
        {
            return new SizeUpdateDto
            {
                SizeId = size.SizeId,
                Number = size.SizeNumber
            };
        }
    }
}
