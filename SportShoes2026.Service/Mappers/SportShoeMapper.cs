using SportShoes2026.Entities;
using SportShoes2026.Service.DTOs.SportShoe;

namespace SportShoes2026.Service.Mappers
{
    public static class SportShoeMapper
    {
        public static SportShoeListDto
            ToListDto(SportShoe shoe)
        {
            return new SportShoeListDto
            {
                SportShoeId = shoe.ShoeId,
                Model = shoe.Model,
                Price = shoe.Price,
                BrandName = shoe.Brand.BrandName,
                GenreName = shoe.Genre.GenreName,
                SportName = shoe.Sport.SportName,
                SizeNumber = shoe.Size.SizeNumber
            };
        }

        public static SportShoeDetailsDto
            ToDetailsDto(SportShoe shoe)
        {
            return new SportShoeDetailsDto
            {
                SportShoeId = shoe.ShoeId,
                Model = shoe.Model,
                Price = shoe.Price,
                BrandName = shoe.Brand.BrandName,
                GenreName = shoe.Genre.GenreName,
                SportName = shoe.Sport.SportName
            };
        }

        public static SportShoe
            ToEntity(SportShoeCreateDto dto)
        {
            return new SportShoe
            {
                Model = dto.Model,
                Price = dto.Price,
                BrandId = dto.BrandId,

                SportId = dto.SportId,
                GenreId = dto.GenreId,
                Active = true
            };
        }

        public static SportShoe
            ToEntity(SportShoeUpdateDto dto)
        {
            return new SportShoe
            {
                ShoeId = dto.SportShoeId,
                Model = dto.Model,
                Price = dto.Price,
                BrandId = dto.BrandId,
                GenreId = dto.GenreId,
                SportId = dto.SportId

            };
        }

        public static SportShoeUpdateDto
            ToUpdateDto(SportShoe shoe)
        {
            return new SportShoeUpdateDto
            {
                SportShoeId = shoe.ShoeId,
                Model = shoe.Model,
                Price = shoe.Price,
                BrandId = shoe.BrandId,
                GenreId = shoe.GenreId,
                SportId = shoe.SportId
            };
        }
    }
}
