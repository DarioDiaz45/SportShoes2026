

using Microsoft.Extensions.DependencyInjection;
using SportShoes2026.IoC;
using SportShoes2026.Service.DTOs.Brand;
using SportShoes2026.Service.DTOs.Sport;
using SportShoes2026.Service.DTOs.SportShoe;
using SportShoes2026.Service.Interfaces;

internal class Program
{
    static IServiceProvider provider =
        DependencyInjectionContainer.Configure();




    static void Main(string[] args)
    {
        do
        {
            Console.Clear();

            Console.WriteLine("SPORT SHOES MANAGER");
            Console.WriteLine("1. Brands");
            Console.WriteLine("2. Sports");
            Console.WriteLine("3. Size");
            Console.WriteLine("4. Sport Shoes");
            Console.WriteLine("0. Exit");

            Console.Write("Select option: ");

            var op = Console.ReadLine();

            switch (op)
            {
                case "1":
                    BrandsMenu();
                    break;
                case "2":
                    SportsMenu();
                    break;
                case "3":
                    SizeMenu();
                    break;
                case "4":
                    SportShoesMenu();
                    break;
                case "0":
                    return;
            }

        } while (true);
    }

    private static void SportShoesMenu()
    {
        using var scope = provider.CreateScope();

        var service =
            scope.ServiceProvider
                .GetRequiredService<ISportShoeService>();

        var brandService =
            scope.ServiceProvider
                .GetRequiredService<IBrandService>();

        var sizeService =
            scope.ServiceProvider
                .GetRequiredService<ISizeService>();

        var sportService =
            scope.ServiceProvider
                .GetRequiredService<ISportService>();

        do
        {
            Console.Clear();

            Console.WriteLine("SPORT SHOES");
            Console.WriteLine("1 - List");
            Console.WriteLine("2 - Add");
            Console.WriteLine("3 - Update");
            Console.WriteLine("4 - Delete");
            Console.WriteLine("5 - Details");
            Console.WriteLine("0 - Back");

            Console.Write("Option: ");

            var op = Console.ReadLine();

            switch (op)
            {
                case "1":
                    ListSportShoes(service);
                    break;

                case "2":
                    AddSportShoe(service, brandService, sizeService, sportService);
                    break;

                case "3":
                    UpdateSportShoe(service, brandService, sizeService, sportService);
                    break;

                case "4":
                    DeleteSportShoe(service);
                    break;

                case "5":
                    ShowSportShoeDetails(service);
                    break;

                case "0":
                    return;
            }

        } while (true);
    }

    private static void ShowSportShoeDetails(ISportShoeService service)
    {
        Console.Clear();

        ListSportShoes(service);

        Console.WriteLine();

        Console.Write("Select Sport Shoe Id: ");

        int id = int.Parse(
            Console.ReadLine()!);

        var result =
            service.GetDetails(id);

        if (result.IsFailure)
        {
            ShowErrors(result.Errors);
            return;
        }

        var shoe = result.Value!;

        Console.WriteLine();
        Console.WriteLine(
            $"Model: {shoe.Model}");

        Console.WriteLine(
            $"Brand: {shoe.BrandName}");

        Console.WriteLine(
            $"Sport: {shoe.SportName}");

        Console.WriteLine(
            $"Size: {shoe.SizeNumber}");

        Console.WriteLine(
            $"Price: {shoe.Price}");

        Console.WriteLine(
            $"Release: {shoe.ReleaseDate:d}");

        Console.ReadLine();

    }

    private static void DeleteSportShoe(ISportShoeService service)
    {
        Console.Clear();

        ListSportShoes(service);

        Console.WriteLine();

        Console.Write("Id: ");

        int id = int.Parse(
            Console.ReadLine()!);

        var result = service.Delete(id);

        if (result.IsFailure)
        {
            ShowErrors(result.Errors);
        }
        else
        {
            Console.WriteLine(
                "Deleted correctly Shoe!!");
        }
        Console.ReadLine();
    }

    private static void UpdateSportShoe(ISportShoeService service, IBrandService brandService, ISizeService sizeService, ISportService sportService)
    {
        Console.Clear();

        ListSportShoes(service);

        Console.WriteLine();

        Console.Write("Select Id: ");

        int id = int.Parse(
            Console.ReadLine()!);

        var resultDto =
            service.GetForUpdate(id);

        if (resultDto.IsFailure)
        {
            ShowErrors(resultDto.Errors);
            return;
        }

        var dto = resultDto.Value!;

        Console.Write(
            $"Model ({dto.Model}): ");

        var model = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(model))
        {
            dto.Model = model;
        }

        Console.Write(
            $"Price ({dto.Price}): ");

        if (decimal.TryParse(
            Console.ReadLine(),
            out decimal price))
        {
            dto.Price = price;
        }

        var result = service.Update(dto);

        if (result.IsFailure)
        {
            ShowErrors(result.Errors);
        }
        else
        {
            Console.WriteLine(
                "Updated correctly Shoe!!");
        }
        Console.ReadLine();

    }

    private static void AddSportShoe(ISportShoeService service,IBrandService brandService,ISizeService sizeService,ISportService sportService)
    {
        Console.Clear();
        Console.WriteLine("=====Add New Shoe=====");

        var dto = new SportShoeCreateDto();

        Console.Write("Model: ");
        dto.Model = Console.ReadLine()!;

        Console.Write("Price: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal price))
        {
            Console.WriteLine("Invalid price!!!.");
            return;
        }
        dto.Price = price;

        
        dto.ReleaseDate = DateTime.Now;

        
        Console.WriteLine("=====GENDERS=====");
        var genreResult = service.GetGenres();
        if (genreResult.IsSuccess)
        {
            foreach (var genre in genreResult.Value!)
            {
                Console.WriteLine($"{genre.GenreId} - {genre.GenreName}");
            }
        }
        Console.Write("Select Gender ID: ");
        dto.GenreId = int.Parse(Console.ReadLine()!);

        
        Console.WriteLine("=====BRANDS=====");
        var brandResult = brandService.GetAll();
        if (brandResult.IsSuccess)
        {
            foreach (var brand in brandResult.Value!)
            {
                Console.WriteLine($"{brand.BrandId} - {brand.BrandName}");
            }
        }
        Console.Write("Select Brand ID: ");
        dto.BrandId = int.Parse(Console.ReadLine()!);

        
        Console.WriteLine("=====BRANDS=====");
        var sizeResult = sizeService.GetAll();
        if (sizeResult.IsSuccess)
        {
            foreach (var size in sizeResult.Value!)
            {
                Console.WriteLine($"{size.SizeId} - {size.Number}");
            }
        }
        Console.Write("Select Size ID: ");
        dto.SizeId = int.Parse(Console.ReadLine()!);

        Console.Write("Description: "); 
        dto.Description = Console.ReadLine()!;

        
        Console.WriteLine("=====SPORTS=====");
        var sportResult = sportService.GetAll();
        if (sportResult.IsSuccess)
        {
            foreach (var sport in sportResult.Value!)
            {
                Console.WriteLine($"{sport.SportId} - {sport.SportName}");
            }
        }
        Console.Write("Select Sport ID: ");
        dto.SportId = int.Parse(Console.ReadLine()!);

        
        var result = service.Add(dto);

        if (result.IsFailure)
        {
            
            ShowErrors(result.Errors);
        }
        else
        {
            Console.WriteLine("Shoe successfully added!!!");
        }

        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();
    }

    private static void ListSportShoes(ISportShoeService service)
    {
        Console.Clear();

        var result = service.GetAll();

        if (result.IsFailure)
        {
            ShowErrors(result.Errors);
            return;
        }

        foreach (var shoe in result.Value!)
        {
            Console.WriteLine(
                $"Id: {shoe.SportShoeId,-5}" +
                $"Model: {shoe.Model,-20}" +
                $"Brand: {shoe.BrandName,-15}" +
                $"Sport: {shoe.SportName,-15}" +
                $"Size: {shoe.SizeNumber,-10}" +
                $"Price: {shoe.Price}");
        }
        Console.ReadLine();
    }

    //ACA EMPIEZA EL MENU DE LOS TAMAÑOS
    private static void SizeMenu()
    {
        using var scope = provider.CreateScope();

        var service =
            scope.ServiceProvider
                .GetRequiredService<ISizeService>();

        do
        {
            Console.Clear();

            Console.WriteLine("=====SIZES=====");
            Console.WriteLine("1 - List Sizes");
            Console.WriteLine("2 - Update Size");
            Console.WriteLine("0 - Back");

            Console.Write("Select option: ");

            var op = Console.ReadLine();

            switch (op)
            {
                case "1":
                    ListSizes(service);
                    break;

                case "2":
                    UpdateSize(service);
                    break;

                case "0":
                    return;
            }

        } while (true);
    }

    private static void UpdateSize(ISizeService service)
    {
        Console.Clear();

        Console.WriteLine("UPDATE SIZE");
        Console.WriteLine();

        var sizesResult = service.GetAll();

        if (sizesResult.IsFailure)
        {
            ShowErrors(sizesResult.Errors);
            return;
        }

        foreach (var size in sizesResult.Value!)
        {
            Console.WriteLine(
                $"{size.SizeId} - {size.Number}");
        }

        Console.WriteLine();

        Console.Write("Size Id: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid Id");
            Pause();
            return;
        }

        var sizeResult = service.GetForUpdate(id);

        if (sizeResult.IsFailure)
        {
            ShowErrors(sizeResult.Errors);
            return;
        }

        var dto = sizeResult.Value!;

        Console.Write(
            $"Number ({dto.Number}): ");

        if (decimal.TryParse(
            Console.ReadLine(),
            out decimal number))
        {
            dto.Number = number;
        }

        var result = service.Update(dto);

        if (result.IsFailure)
        {
            ShowErrors(result.Errors);
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine("Size updated");
        }

        Console.ReadLine();
    }

    private static void ListSizes(ISizeService service)
    {
        Console.Clear();

        Console.WriteLine("LIST OF SIZES");
        Console.WriteLine();

        var result = service.GetAll();

        if (result.IsFailure)
        {
            ShowErrors(result.Errors);
            return;
        }

        foreach (var size in result.Value!)
        {
            Console.WriteLine(
                $"Id: {size.SizeId,-5}" +
                $"Number: {size.Number,-10}");
        }
        Console.ReadLine();
    }



    //ACA EMPIEZA EL MENU DE LOS DEPORTES
    private static void SportsMenu()
    {
        using var scope = provider.CreateScope();

        var service =
            scope.ServiceProvider
                .GetRequiredService<ISportService>();

        do
        {
            Console.Clear();

            Console.WriteLine("SPORTS");
            Console.WriteLine("1 - List Sports");
            Console.WriteLine("2 - Add Sport");
            Console.WriteLine("3 - Delete Sport");
            Console.WriteLine("4 - Update Sport");
            Console.WriteLine("0 - Back");

            Console.Write("Select option: ");

            var op = Console.ReadLine();

            switch (op)
            {
                case "1":
                    ListSports(service);
                    break;

                case "2":
                    AddSport(service);
                    break;

                case "3":
                    DeleteSport(service);
                    break;

                case "4":
                    UpdateSport(service);
                    break;

                case "0":
                    return;
            }

        } while (true);
    }


    private static void ListSports(
        ISportService service)
    {
        Console.Clear();

        Console.WriteLine("List Of Sports");
        Console.WriteLine();

        var result = service.GetAll();

        if (result.IsFailure)
        {
            ShowErrors(result.Errors);
            return;
        }

        foreach (var sport in result.Value!)
        {
            Console.WriteLine(
                $"Id: {sport.SportId,-5}" +
                $"Name: {sport.SportName,-20}");
        }

        Pause();
    }



    private static void AddSport(
        ISportService service)
    {
        Console.Clear();

        Console.WriteLine("Add Sport");
        Console.WriteLine();

        var dto = new SportCreateDto();

        Console.Write("Name Sport: ");
        dto.SportName = Console.ReadLine() ?? "";

        var result = service.Add(dto);

        if (result.IsFailure)
        {
            ShowErrors(result.Errors);
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine("Sport added successfully");
        }

        Pause();
    }



    private static void DeleteSport(
        ISportService service)
    {
        Console.Clear();

        Console.WriteLine("Delete Sport Correctly!!");
        Console.WriteLine();

        var sportsResult = service.GetAll();

        if (sportsResult.IsFailure)
        {
            ShowErrors(sportsResult.Errors);
            return;
        }

        foreach (var sport in sportsResult.Value!)
        {
            Console.WriteLine(
                $"{sport.SportId} - {sport.SportName}");
        }

        Console.WriteLine();

        Console.Write("Sport Id: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid Id!!!");
            Pause();
            return;
        }

        var result = service.Delete(id);

        if (result.IsFailure)
        {
            ShowErrors(result.Errors);
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine("Sport deleted!!!");
        }

        Pause();
    }



    private static void UpdateSport(
        ISportService service)
    {
        Console.Clear();

        Console.WriteLine("Update Sport");
        Console.WriteLine();

        var sportsResult = service.GetAll();

        if (sportsResult.IsFailure)
        {
            ShowErrors(sportsResult.Errors);
            return;
        }

        foreach (var sport in sportsResult.Value!)
        {
            Console.WriteLine(
                $"{sport.SportId} - {sport.SportName}");
        }

        Console.WriteLine();

        Console.Write("Sport Id: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid Id");
            Pause();
            return;
        }

        var sportResult = service.GetForUpdate(id);

        if (sportResult.IsFailure)
        {
            ShowErrors(sportResult.Errors);
            return;
        }

        var dto = sportResult.Value!;

        Console.Write(
            $"Name ({dto.SportName}): ");

        var newName = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(newName))
        {
            dto.SportName = newName;
        }

        var result = service.Update(dto);

        if (result.IsFailure)
        {
            ShowErrors(result.Errors);
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine("Sport updated");
        }

        Pause();
    }

    //BRANDS

    private static void BrandsMenu()
    {
        using var scope = provider.CreateScope();

        var service =
            scope.ServiceProvider
                .GetRequiredService<IBrandService>();

        do
        {
            Console.Clear();

            Console.WriteLine("========Brands========");
            Console.WriteLine("1 - List Brands");
            Console.WriteLine("2 - Add Brand");
            Console.WriteLine("3 - Delete Brand");
            Console.WriteLine("4 - Update Brand");
            Console.WriteLine("0 - Back");

            Console.Write("Select option: ");

            var op = Console.ReadLine();

            switch (op)
            {
                case "1":
                    ListBrands(service);
                    break;

                case "2":
                    AddBrand(service);
                    break;

                case "3":
                    DeleteBrand(service);
                    break;

                case "4":
                    UpdateBrand(service);
                    break;

                case "0":
                    return;
            }

        } while (true);
    }

   

    private static void ListBrands(
        IBrandService service)
    {
        Console.Clear();

        Console.WriteLine("List Of Brands");
        Console.WriteLine();

        var result = service.GetAll();

        if (result.IsFailure)
        {
            ShowErrors(result.Errors);
            return;
        }

        foreach (var brand in result.Value!)
        {
            Console.WriteLine(
                $"Id: {brand.BrandId,-5}" +
                $"Name: {brand.BrandName,-20}");
        }

        Pause();
    }



    private static void AddBrand(
        IBrandService service)
    {
        Console.Clear();

        Console.WriteLine("Add Brand");
        Console.WriteLine();

        var dto = new BrandCreateDto();

        Console.Write("Name: ");
        dto.BrandName = Console.ReadLine() ?? "";

        var result = service.Add(dto);

        if (result.IsFailure)
        {
            ShowErrors(result.Errors);
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine("Brand added successfully");
        }

        Pause();
    }



    private static void DeleteBrand(
        IBrandService service)
    {
        Console.Clear();

        Console.WriteLine("Delete Brand");
        Console.WriteLine();

        var brandsResult = service.GetAll();

        if (brandsResult.IsFailure)
        {
            ShowErrors(brandsResult.Errors);
            return;
        }

        foreach (var brand in brandsResult.Value!)
        {
            Console.WriteLine(
                $"{brand.BrandId} - {brand.BrandName}");
        }

        Console.WriteLine();

        Console.Write("Brand Id: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid Id");
            Pause();
            return;
        }

        var result = service.Delete(id);

        if (result.IsFailure)
        {
            ShowErrors(result.Errors);
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine("Brand deleted");
        }

        Pause();
    }



    private static void UpdateBrand(
        IBrandService service)
    {
        Console.Clear();

        Console.WriteLine("Update Brand");
        Console.WriteLine();

        var brandsResult = service.GetAll();

        if (brandsResult.IsFailure)
        {
            ShowErrors(brandsResult.Errors);
            return;
        }

        foreach (var brand in brandsResult.Value!)
        {
            Console.WriteLine(
                $"{brand.BrandId} - {brand.BrandName}");
        }

        Console.WriteLine();

        Console.Write("Brand Id: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid Id");
            Pause();
            return;
        }

        var brandResult = service.GetForUpdate(id);

        if (brandResult.IsFailure)
        {
            ShowErrors(brandResult.Errors);
            return;
        }

        var dto = brandResult.Value!;

        Console.Write(
            $"Name ({dto.BrandName}): ");

        var newName = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(newName))
        {
            dto.BrandName = newName;
        }
        var newCountry = Console.ReadLine();



        var result = service.Update(dto);

        if (result.IsFailure)
        {
            ShowErrors(result.Errors);
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine("Brand updated");
        }

        Pause();
    }



    private static void ShowErrors(
        List<string> errors)
    {
        Console.WriteLine();

        foreach (var error in errors)
        {
            Console.WriteLine(error);
        }

        Pause();
    }

    private static void Pause()
    {
        Console.WriteLine();
        Console.WriteLine("Press any key...");
        Console.ReadKey();
    }
}
