using FluentValidation;
using SportShoes2026.Data;
using SportShoes2026.Entities;
using SportShoes2026.Service.Common;
using SportShoes2026.Service.DTOs.SportShoe;
using SportShoes2026.Service.Interfaces;

namespace SportShoes2026.Service.Services
{
    public class SportShoeService : ISportShoeService
    {
        private readonly IUnitOfWork _uow;

        private readonly IValidator<SportShoe>
            _validator;

        public SportShoeService(
            IUnitOfWork uow,
            IValidator<SportShoe> validator)
        {
            _uow = uow;
            _validator = validator;
        }


        public Result<List<SportShoeListDto>>GetAll()
        {
            var shoes = _uow.SportShoes
                .GetAll()
                .Select(s => new SportShoeListDto
                {
                    SportShoeId = s.ShoeId,
                    Model = s.Model,
                    Price = s.Price,
                    BrandName = s.Brand.BrandName,
                    GenreName = s.Genre.GenreName,
                    SportName = s.Sport.SportName,
                    SizeNumber = s.Size != null ? s.Size.SizeNumber : 0
                })
                .ToList();

            return Result<List<SportShoeListDto>>
                .Success(shoes);
        }


        public Result<SportShoeDetailsDto>
            GetDetails(int id)
        {
            var shoe = _uow.SportShoes
                .Query()
                .Where(s => s.ShoeId == id)
                .Select(s => new SportShoeDetailsDto
                {
                    SportShoeId = s.ShoeId,
                    Model = s.Model,
                    Price = s.Price,
                    BrandName = s.Brand.BrandName,
                    GenreName = s.Genre.GenreName,
                    SportName = s.Sport.SportName
                })
                .FirstOrDefault();

            if (shoe == null)
            {
                return Result<SportShoeDetailsDto>
                    .Failure("Sport shoe not found");
            }

            return Result<SportShoeDetailsDto>
                .Success(shoe);
        }


        public Result Add(
            SportShoeCreateDto dto)
        {
            var shoe = new SportShoe
            {
                Model = dto.Model,
                Price = dto.Price,
                BrandId = dto.BrandId,
                Description = dto.Description,
                GenreId = dto.GenreId,
                SportId = dto.SportId,
                SizeId = dto.SizeId,
                Active = true
            };

            var validation =
                _validator.Validate(shoe);

            if (!validation.IsValid)
            {
                return Result.Failure(
                    validation.Errors
                        .Select(e => e.ErrorMessage)
                        .ToList());
            }

            if (_uow.SportShoes
                .ExistSameSportShoe(
                    shoe.Model,
                    shoe.BrandId,
                    shoe.GenreId))
            {
                return Result.Failure(
                    "Sport shoe already exists");
            }

            try
            {
                _uow.SportShoes.Add(shoe);

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
            var shoe =
                _uow.SportShoes.GetById(id);

            if (shoe == null)
            {
                return Result.Failure(
                    "Sport shoe not found");
            }

            try
            {
                _uow.SportShoes.Delete(id);

                _uow.Save();

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }


        public Result<SportShoeUpdateDto>
            GetForUpdate(int id)
        {
            var shoe =
                _uow.SportShoes.GetById(id);

            if (shoe == null)
            {
                return Result<SportShoeUpdateDto>
                    .Failure("Sport shoe not found");
            }

            var dto = new SportShoeUpdateDto
            {
                SportShoeId = shoe.ShoeId,
                Model = shoe.Model,
                Price = shoe.Price,
                BrandId = shoe.BrandId,
                GenreId = shoe.GenreId,
                SportId = shoe.SportId
            };

            return Result<SportShoeUpdateDto>
                .Success(dto);
        }


        public Result Update(
            SportShoeUpdateDto dto)
        {
            var shoe =
                _uow.SportShoes.GetById(
                    dto.SportShoeId);

            if (shoe == null)
            {
                return Result.Failure(
                    "Sport shoe not found");
            }

            shoe.Model = dto.Model;
            shoe.Price = dto.Price;
            shoe.BrandId = dto.BrandId;
            shoe.GenreId = dto.GenreId;
            shoe.SportId = dto.SportId;

            var validation =
                _validator.Validate(shoe);

            if (!validation.IsValid)
            {
                return Result.Failure(
                    validation.Errors
                        .Select(e => e.ErrorMessage)
                        .ToList());
            }

            if (_uow.SportShoes
                .ExistSameSportShoe(
                    shoe.Model,
                    shoe.BrandId,
                    shoe.GenreId,
                    shoe.ShoeId))
            {
                return Result.Failure(
                    "Sport shoe already exists!!");
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

        public Result<List<Genre>> GetGenres()
        {
            var genres = _uow.Genres.GetAll().ToList();
            return Result<List<Genre>>.Success(genres);
        }
    }
}
