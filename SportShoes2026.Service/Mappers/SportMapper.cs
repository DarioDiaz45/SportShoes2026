using SportShoes2026.Entities;
using SportShoes2026.Service.DTOs.Sport;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportShoes2026.Service.Mappers
{
    public static class SportMapper
    {
        public static Sport ToEntity(SportCreateDto dto)
        {
            return new Sport
            {
                SportName = dto.SportName,
                Active = true
            };
        }

        public static SportListDto ToListDto(
            Sport sport)
        {
            return new SportListDto
            {
                SportId = sport.SportId,
                SportName = sport.SportName
            };
        }

        public static SportUpdateDto ToUpdateDto(
            Sport sport)
        {
            return new SportUpdateDto
            {
                SportId = sport.SportId,
                SportName = sport.SportName
            };
        }
    }
}
