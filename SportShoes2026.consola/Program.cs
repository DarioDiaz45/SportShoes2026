

using Microsoft.Extensions.DependencyInjection;
using SportShoes2026.Entities;
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

            Console.WriteLine("========SPORT SHOES MANAGER=========");
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
            Console.WriteLine("1 - List The Shoes");
            Console.WriteLine("2 - Add The Shoes");
            Console.WriteLine("3 - Update The Shoes");
            Console.WriteLine("4 - Delete The Shoes");
            Console.WriteLine("5 - Details The Shoes");
            Console.WriteLine("6 - Filters & Reports");
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
                case "6":
                    ReportsMenu(service,brandService,sportService,sizeService);
                    break;

                case "0":
                    return;
            }

        } while (true);
    }

    private static void ReportsMenu(ISportShoeService service,IBrandService brandService,ISportService sportService,ISizeService sizeService)
    {
        do
        {
            Console.Clear();

            Console.WriteLine("FILTERS & REPORTS");
            Console.WriteLine();

            Console.WriteLine("1 - Filter By Brand");
            Console.WriteLine("2 - Filter By Sport");
            Console.WriteLine("3 - Filter By Size");

            Console.WriteLine("======================");

            Console.WriteLine("4 - Order By Model");
            Console.WriteLine("5 - Order By Price");
            Console.WriteLine("6 - Order By Brand");

            Console.WriteLine("0 - Back");

            var op = Console.ReadLine();

            switch (op)
            {
                case "1":
                    FilterByBrand( service,brandService);
                    break;

                case "2":
                    FilterBySport(service,sportService);
                    break;

                case "3":
                    FilterBySize(service,sizeService);
                    break;

                case "4":
                    ShowOrderedByModel(service);
                    break;

                case "5":
                    ShowOrderedByPrice(service);
                    break;

                case "6":
                    ShowOrderedByBrand(service);
                    break;

                case "0":
                    return;
            }

        } while (true);
    }

    private static void ShowOrderedByBrand(ISportShoeService service)
    {
        Console.Clear();

        Console.WriteLine("SHOES ORDERED BY BRAND");
        Console.WriteLine();

        var result =service.OrderByBrand();

        if (result.IsFailure)
        {
            ShowErrors(result.Errors);
            return;
        }

        ShowSportShoes(result.Value!);

        Console.ReadLine();
    }

    private static void ShowOrderedByPrice(ISportShoeService service)
    {
        Console.Clear();

        Console.WriteLine("SHOES ORDERED BY PRICE");
        Console.WriteLine();

        var result =service.OrderByPrice();

        if (result.IsFailure)
        {
            ShowErrors(result.Errors);
            return;
        }

        ShowSportShoes(result.Value!);

        Console.ReadLine();
    }

    private static void ShowOrderedByModel(ISportShoeService service)
    {
        Console.Clear();

        Console.WriteLine("SHOES ORDERED BY MODEL");
        Console.WriteLine();

        var result =service.OrderByModel();

        if (result.IsFailure)
        {
            ShowErrors(result.Errors);
            return;
        }

        ShowSportShoes(result.Value!);

        Console.ReadLine();
    }

    private static void FilterBySize(ISportShoeService service, ISizeService sizeService)
    {
        Console.Clear();

        Console.WriteLine("FILTER SHOES BY SIZE");
        Console.WriteLine();

        ShowSizes(sizeService);

        Console.WriteLine();

        Console.Write("Select Size Id: ");

        int sizeId = int.Parse(
            Console.ReadLine()!);

        var result =
            service.GetBySize(sizeId);

        if (result.IsFailure)
        {
            ShowErrors(result.Errors);
            return;
        }

        ShowSportShoes(result.Value!);

        Console.ReadLine();
    }

    private static void ShowSizes(ISizeService sizeService)
    {
        var result = sizeService.GetAll();

        if (result.IsFailure)
        {
            ShowErrors(result.Errors);
            return;
        }

        foreach (var size in result.Value!)
        {
            Console.WriteLine($"{size.SizeId,-5} | " +$"{size.Number,-10}");
        }
    }

    private static void FilterBySport(ISportShoeService service, ISportService sportService)
    {
        Console.Clear();

        Console.WriteLine("FILTER SHOES BY SPORT");
        Console.WriteLine();

        ShowSports(sportService);

        Console.WriteLine();

        Console.Write("Select Sport Id: ");

        int sportId = int.Parse(Console.ReadLine()!);

        var result =service.GetBySport(sportId);

        if (result.IsFailure)
        {
            ShowErrors(result.Errors);
            return;
        }

        ShowSportShoes(result.Value!);

        Console.ReadLine();
    }

    private static void ShowSports(ISportService sportService)
    {
        var result = sportService.GetAll();

        if (result.IsFailure)
        {
            ShowErrors(result.Errors);
            return;
        }

        foreach (var sport in result.Value!)
        {
            Console.WriteLine($"{sport.SportId,-5} | " +$"{sport.SportName,-20}");
        }
    }

    private static void FilterByBrand(ISportShoeService service, IBrandService brandService)
    {
        Console.Clear();

        Console.WriteLine("FILTER SHOES BY BRAND");
        Console.WriteLine();

        ShowBrands(brandService);

        Console.WriteLine();

        Console.Write("Select Brand Id: ");

        int brandId = int.Parse(Console.ReadLine()!);

        var result = service.GetByBrand(brandId);

        if (result.IsFailure)
        {
            ShowErrors(result.Errors);
            return;
        }

        ShowSportShoes(result.Value!);

        Console.ReadLine();
    }

    private static void ShowBrands(IBrandService brandService)
    {
        var result = brandService.GetAll();

        if (result.IsFailure)
        {
            ShowErrors(result.Errors);
            return;
        }

        foreach (var brand in result.Value!)
        {
            Console.WriteLine($"{brand.BrandId,-5} | " +$"{brand.BrandName,-20}");
        }
    }

    private static void ShowSportShoeDetails(ISportShoeService service)
    {
        Console.Clear();

        ListSportShoes(service);

        Console.WriteLine();

        Console.Write("Select Sport Shoe Id: ");

        int id = int.Parse(Console.ReadLine()!);

        var result =service.GetDetails(id);

        if (result.IsFailure)
        {
            ShowErrors(result.Errors);
            return;
        }

        var shoe = result.Value!;

        Console.WriteLine();
        Console.WriteLine($"Model: {shoe.Model}");

        Console.WriteLine($"Brand: {shoe.BrandName}");

        Console.WriteLine($"Sport: {shoe.SportName}");

        Console.WriteLine($"Size: {shoe.SizeNumber}");

        Console.WriteLine($"Price: {shoe.Price}");

        Console.ReadLine();

    }

    private static void DeleteSportShoe(ISportShoeService service)
    {
        Console.Clear();

        ListSportShoes(service);

        Console.WriteLine();

        Console.Write("Select Id Sport Shoe : ");

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
        Console.Write("Select Id Sport Shoe : ");
        int id = int.Parse(Console.ReadLine()!);

        var resultDto =service.GetForUpdate(id);

        if (resultDto.IsFailure)
        {
            ShowErrors(resultDto.Errors);
            return;
        }

        var dto = resultDto.Value!;

        Console.Write($"Model ({dto.Model}): ");

        var model = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(model))
        {
            dto.Model = model;
        }

        Console.Write($"Price ({dto.Price}): ");

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
            Console.WriteLine("Updated correctly Shoe!!");
        }
        Console.ReadLine();

    }

    private static void AddSportShoe(ISportShoeService service, IBrandService brandService, ISizeService sizeService, ISportService sportService)
    {
        Console.Clear();
        Console.WriteLine("=====Add New Shoe=====");

        var dto = new SportShoeCreateDto();

        Console.Write("Add The Model: ");
        dto.Model = Console.ReadLine()!;

        Console.Write("Add The Price: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal price))
        {
            Console.WriteLine("Invalid price, Try Again!!!.");
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
                Console.WriteLine($"{size.SizeId} | {size.Number}");
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
                Console.WriteLine($"{sport.SportId} | {sport.SportName}");
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

       

        Console.WriteLine(new string('-', 85));

        ShowSportShoes(result.Value!);
        

        Console.ReadLine();
    }

    private static void ShowSportShoes(List<SportShoeListDto> shoes)
    {
        Console.WriteLine(
            $"{"Id",-5} | " +
            $"{"Model",-20} | " +
            $"{"Brand",-15} | " +
            $"{"Sport",-15} | " +
            $"{"Size",-10} | " +
            $"{"Price",-10}");

        Console.WriteLine(new string('-', 85));

        foreach (var shoe in shoes)
        {
            Console.WriteLine(
                $"{shoe.SportShoeId,-5} | " +
                $"{shoe.Model,-20} | " +
                $"{shoe.BrandName,-15} | " +
                $"{shoe.SportName,-15} | " +
                $"{shoe.SizeNumber,-10} | " +
                $"{shoe.Price,-10:C}");
        }
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

        Console.WriteLine("====UPDATE SIZE====");
        Console.WriteLine();

        var sizesResult = service.GetAll();

        if (sizesResult.IsFailure)
        {
            ShowErrors(sizesResult.Errors);
            return;
        }

        foreach (var size in sizesResult.Value!)
        {
            Console.WriteLine($"[ID: {size.SizeId}] | Number: {size.Number,-10}");
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

        Console.Write($"Number ({dto.Number}): ");

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
            Console.WriteLine("Size updated!!!");
        }

        Console.ReadLine();
    }

    private static void ListSizes(ISizeService service)
    {
        Console.Clear();

        Console.WriteLine("====LIST OF SIZES====");
        Console.WriteLine();

        var result = service.GetAll();

        if (result.IsFailure)
        {
            ShowErrors(result.Errors);
            return;
        }

        foreach (var size in result.Value!)
        {
            Console.WriteLine($"[ID: {size.SizeId}] | Number: {size.Number,-10}");
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

            Console.WriteLine("=====SPORTS=====");
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
            Console.WriteLine($"[ID: {sport.SportId}] | Name: {sport.SportName,-20}");
        }

        Pause();
    }



    private static void AddSport(
        ISportService service)
    {
        Console.Clear();

        Console.WriteLine("====Add Sport====");
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
            Console.WriteLine("Sport added successfully!!!");
        }

        Pause();
    }



    private static void DeleteSport(
        ISportService service)
    {
        Console.Clear();

        Console.WriteLine("====Delete Sport====");
        Console.WriteLine();

        var sportsResult = service.GetAll();

        if (sportsResult.IsFailure)
        {
            ShowErrors(sportsResult.Errors);
            return;
        }

        foreach (var sport in sportsResult.Value!)
        {
            Console.WriteLine($"[ID: {sport.SportId}] | Name: {sport.SportName,-20}");
        }

        Console.WriteLine();

        Console.Write("Sport Id: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid Id Try Again!!!");
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

        Console.WriteLine("====Update Sport====");
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
                $"{sport.SportId} | {sport.SportName,-20}");
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
            Console.WriteLine("Sport updated!!!");
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
            Console.WriteLine($"[ID: {brand.BrandId}] | Name: {brand.BrandName,-20}");
        }

        Pause();
    }



    private static void AddBrand(
        IBrandService service)
    {
        Console.Clear();

        Console.WriteLine("====Add Brand====");
        Console.WriteLine();

        var dto = new BrandCreateDto();

        Console.Write("Brand name: ");
        dto.BrandName = Console.ReadLine() ?? "";

        var result = service.Add(dto);

        if (result.IsFailure)
        {
            ShowErrors(result.Errors);
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine("Brand added successfully!!");
        }

        Console.ReadLine();
    }



    private static void DeleteBrand(IBrandService service)
    {
        Console.Clear();

        Console.WriteLine("====Delete Brand====");
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
                $"[{brand.BrandId}] | {brand.BrandName}");
        }

        Console.WriteLine();

        Console.Write("Enter the brand ID: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID, please try again");
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
            Console.WriteLine("Brand erased correctly!!");
        }

        Pause();
    }



    private static void UpdateBrand(
        IBrandService service)
    {
        Console.Clear();

        Console.WriteLine("====Update Brand====");
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
                $"[{brand.BrandId}] | {brand.BrandName}");
        }

        Console.WriteLine();

        Console.Write("Enter the brand ID: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID, please try again");
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
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}
