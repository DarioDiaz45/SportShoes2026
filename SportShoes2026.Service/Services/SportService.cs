using FluentValidation;
using SportShoes2026.Data;
using SportShoes2026.Entities;
using SportShoes2026.Service.Common;
using SportShoes2026.Service.DTOs.Sport;
using SportShoes2026.Service.Interfaces;
using SportShoes2026.Service.Mappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportShoes2026.Service.Services
{
    public class SportService : ISportService
    {
        private readonly IUnitOfWork _uow;

        private readonly IValidator<Sport> _validator;

        public SportService(
            IUnitOfWork uow,
            IValidator<Sport> validator)
        {
            _uow = uow;
            _validator = validator;
        }

        public Result Add(SportCreateDto dto)
        {
            var sport = SportMapper.ToEntity(dto);

            var validation = _validator.Validate(sport);

            if (!validation.IsValid)
            {
                return Result.Failure(
                    validation.Errors
                        .Select(e => e.ErrorMessage)
                        .ToList());
            }

            if (_uow.Sports
                .ExistSameName(sport.SportName))
            {
                return Result.Failure(
                    "Sport already exists");
            }

            try
            {
                _uow.Sports.Add(sport);

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
            var sport = _uow.Sports.GetById(id);

            if (sport == null)
            {
                return Result.Failure(
                    "Sport not found");
            }

            if (_uow.Sports.HasSportShoes(id))
            {
                return Result.Failure(
                    "Sport has sport shoes");
            }

            try
            {
                _uow.Sports.Delete(id);

                _uow.Save();

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public Result<List<SportListDto>> GetAll()
        {
            var sports = _uow.Sports
                .GetAll()
                .Select(SportMapper.ToListDto)
                .ToList();

            return Result<List<SportListDto>>
                .Success(sports);
        }

        public Result<SportUpdateDto>
            GetForUpdate(int id)
        {
            var sport = _uow.Sports.GetById(id);

            if (sport == null)
            {
                return Result<SportUpdateDto>
                    .Failure("Sport not found");
            }

            return Result<SportUpdateDto>
                .Success(
                    SportMapper.ToUpdateDto(sport));
        }

        public Result Update(SportUpdateDto dto)
        {
            var sport =
                _uow.Sports.GetById(dto.SportId);

            if (sport == null)
            {
                return Result.Failure(
                    "Sport not found");
            }

            sport.SportName = dto.SportName;

            if (_uow.Sports
                .ExistSameName(
                    sport.SportName,
                    sport.SportId))
            {
                return Result.Failure(
                    "Sport already exists");
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
