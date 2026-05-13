using FluentValidation;
using SportShoes2026.Data;
using SportShoes2026.Entities;
using SportShoes2026.Service.Common;
using SportShoes2026.Service.DTOs.Size;
using SportShoes2026.Service.Interfaces;
using SportShoes2026.Service.Mappers;
using System.Drawing;

namespace SportShoes2026.Service.Services
{
    public class SizeService:ISizeService
    {
        private readonly IUnitOfWork _uow;
        private readonly IValidator<SiZe> _validator;

        public SizeService( IUnitOfWork uow,IValidator<SiZe> validator)
        {
            _uow = uow;
            _validator = validator;
        }

        public Result<List<SizeListDto>> GetAll()
        {
            var sizes = _uow.Sizes
            .GetAll()
            .Select(SizeMapper.ToListDto)
            .ToList();

            return Result<List<SizeListDto>>
                .Success(sizes);
        }

        public Result<SizeUpdateDto> GetForUpdate(int id)
        {
            var size = _uow.Sizes.GetById(id);

            if (size == null)
            {
                return Result<SizeUpdateDto>
                    .Failure("Size not found");
            }

            return Result<SizeUpdateDto>
                .Success(
                    SizeMapper.ToUpdateDto(size));
        }

        public Result Update(SizeUpdateDto dto)
        {
            var size =
               _uow.Sizes.GetById(dto.SizeId);

            if (size == null)
            {
                return Result.Failure(
                    "Size not found");
            }

            size.SizeNumber = dto.Number;

            var validation =
                _validator.Validate(size);

            if (!validation.IsValid)
            {
                return Result.Failure(
                    validation.Errors
                        .Select(e => e.ErrorMessage)
                        .ToList());
            }

            if (_uow.Sizes.ExistSameNumber(
                size.SizeNumber,
                size.SizeId))
            {
                return Result.Failure(
                    "Size already exists");
            }

            try
            {
                _uow.Save();

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }
    }
}
