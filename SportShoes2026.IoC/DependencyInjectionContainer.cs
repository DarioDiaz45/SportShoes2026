using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SportShoes2026.Data;
using SportShoes2026.Data.Interfaces;
using SportShoes2026.Data.Repositories;
using SportShoes2026.Entities;
using SportShoes2026.Service.Interfaces;
using SportShoes2026.Service.Services;
using SportShoes2026.Service.Validator;

namespace SportShoes2026.IoC
{
    public static class DependencyInjectionContainer
    {
        public static IServiceProvider Configure()
        {
            var services = new ServiceCollection();



            services.AddDbContext<ShoesDbContext>();


            services.AddScoped<IUnitOfWork, UnitOfWork>();



            services.AddScoped<IBrandRepository, BrandRepository>();

            services.AddScoped<ISportRepository, SportRepository>();

            services.AddScoped<ISizeRepository, SizeRepository>();

            services.AddScoped<ISportShoeRepository, SportShoeRepository>();

            services.AddScoped<IGenreRepository, GenreRepository>();




            services.AddScoped<IBrandService, BrandService>();

            services.AddScoped<ISportService, SportService>();

            services.AddScoped<ISizeService, SizeService>();

            services.AddScoped<ISportShoeService, SportShoeService>();



            services.AddScoped<IValidator<Brand>, BrandValidator>();

            services.AddScoped<IValidator<Sport>, SportValidator>();

            services.AddScoped<IValidator<SiZe>, SizeValidator>();

            services.AddScoped<IValidator<SportShoe>, SportShoeValidator>();

            return services.BuildServiceProvider();
        }
    }
}