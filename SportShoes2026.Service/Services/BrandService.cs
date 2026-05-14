using FluentValidation;
using SportShoes2026.Data;
using SportShoes2026.Entities;
using SportShoes2026.Service.Common;
using SportShoes2026.Service.DTOs.Brand;
using SportShoes2026.Service.Interfaces;
using SportShoes2026.Service.Mappers;

namespace SportShoes2026.Service.Services
{
    public class BrandService : IBrandService
    {
        private readonly IUnitOfWork _uow;

        private readonly IValidator<Brand> _validator;
        public BrandService(IUnitOfWork uow, IValidator<Brand> validator)
        {
            _uow = uow;
            _validator = validator;
        }

        public Result Add(BrandCreateDto dto)
        {
            var brand = BrandMapper.ToEntity(dto);

            var validation = _validator.Validate(brand);

            if (!validation.IsValid)
            {
                return Result.Failure(
                    validation.Errors
                        .Select(e => e.ErrorMessage)
                        .ToList());
            }

            if (_uow.Brands.ExistSameName(brand.BrandName))
            {
                return Result.Failure("Brand already exists");
            }

            try
            {
                _uow.Brands.Add(brand);

                _uow.Save();

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public Result Delete(int id)
        {
            var brand = _uow.Brands.GetById(id);

            if (brand == null)
            {
                return Result.Failure("Brand not found");
            }

            if (_uow.Brands.HasSportShoes(id))
            {
                return Result.Failure(
                    "Cannot delete brand with sport shoes");
            }

            try
            {
                _uow.Brands.Delete(id);

                _uow.Save();

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public Result<List<BrandListDto>> GetAll()
        {
            var brands = _uow.Brands
                .GetAll()
                .Select(BrandMapper.ToListDto)
                .ToList();

            return Result<List<BrandListDto>>.Success(brands);
        }

        public Result<BrandUpdateDto> GetForUpdate(int id)
        {
            var brand = _uow.Brands.GetById(id);

            if (brand == null)
            {
                return Result<BrandUpdateDto>
                    .Failure("Brand not found");
            }

            return Result<BrandUpdateDto>
                .Success(BrandMapper.ToUpdateDto(brand));
        }

        public Result Update(BrandUpdateDto dto)
        {
            var brand = _uow.Brands.GetById(dto.BrandId);

            if (brand == null)
            {
                return Result.Failure("Brand not found");
            }

            brand.BrandName = dto.BrandName;

            if (_uow.Brands.ExistSameName(
                    brand.BrandName,
                    brand.BrandId))
            {
                return Result.Failure("Brand already exists");
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
