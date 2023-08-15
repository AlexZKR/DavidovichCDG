using CDG.BLL.Entities.Enums;
using CDG.BLL.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CDG.DAL.Data;

public class DbSeeder
{
    public static async Task SeedDataAsync(AppDbContext context, ILogger logger, int retry = 0)
    {
        var retryForAvailability = retry;
        try
        {
            context.Database.EnsureCreated();

            if (!await context.KeyCategorys.AnyAsync())
            {
                await context.KeyCategorys.AddRangeAsync(
                    GetPreconfiguredKeyCategorys());

                await context.SaveChangesAsync();
            }
            if (!await context.DigitalKeys.AnyAsync())
            {
                await context.DigitalKeys.AddRangeAsync(
                    GetPreconfiguredDigitalKeys());

                await context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            if (retryForAvailability >= 10) throw;

            retryForAvailability++;

            logger.LogError(message: ex.Message);
            await SeedDataAsync(context, logger, retryForAvailability);
            throw;
        }
    }

    public static async Task SeedIdDataAsync(AppIdentityDbContext idContext, ILogger logger, int retry = 0)
    {
        var retryForAvailability = retry;
        try
        {
            idContext.Database.EnsureCreated();
        }
        catch (Exception ex)
        {
            if (retryForAvailability >= 10) throw;

            retryForAvailability++;

            logger.LogError(ex.Message);
            await SeedIdDataAsync(idContext, logger, retryForAvailability);
            throw;
        }
    }

    private static ApplicationUser[] GetPreconfiguredUsers()
    {
        throw new NotImplementedException();
    }

    private static IEnumerable<KeyCategory> GetPreconfiguredKeyCategorys()
    {
        return new List<KeyCategory>()
        {
            new KeyCategory
            {
                Name = "Антивирусы",
                Description = "Антивирусы – это программы, которые защищают ваш компьютер от вредоносных атак, таких как вирусы, трояны, шпионское ПО и рекламное ПО. Антивирусы могут сканировать, обнаруживать и удалять вредоносные файлы, а также предотвращать их появление в будущем. Антивирусы также могут улучшить производительность и безопасность вашего компьютера, а также защитить вашу личную информацию и конфиденциальность.",
            },
            new KeyCategory
            {
                Name = "Игры Steam",
                Description = "Игры Steam – это категория интернет-магазина цифровых ключей, в которой вы можете купить ключи активации для игр, распространяемых через платформу Steam. Steam – это популярный сервис для цифрового распространения, управления правами и общения в играх, разработанный компанией Valve. Steam предлагает огромный выбор игр различных жанров и стилей, таких как экшен, приключения, стратегии, симуляторы, спорт, инди и многое другое. Вы можете найти игры как от известных разработчиков и издателей, так и от независимых студий и авторов.",
            },
            new KeyCategory
            {
                Name = "VPN",
                Description = "VPN – это категория интернет-магазина цифровых ключей, в которой вы можете купить ключи активации для VPN-сервисов. VPN – это сокращение от Virtual Private Network, что означает виртуальная частная сеть. VPN – это технология, которая позволяет создавать зашифрованные и безопасные соединения между вашим компьютером и удаленным сервером, который находится в другой стране или регионе. VPN – это полезный инструмент для тех, кто хочет обеспечить свою приватность и анонимность в интернете, а также получить доступ к заблокированным или ограниченным сайтам и сервисам.",
            },
            new KeyCategory
            {
                Name = "Программы Windows",
                Description = "Программы Windows – это категория интернет-магазина цифровых ключей, в которой вы можете купить ключи активации для различных программ, работающих на операционной системе Windows. Windows – это самая популярная и распространенная операционная система в мире, разработанная компанией Microsoft. Windows предоставляет удобный и функциональный интерфейс для работы с компьютером, а также поддерживает множество приложений и игр.",
            },
        };
    }

    private static IEnumerable<DigitalKey> GetPreconfiguredDigitalKeys()
    {
        return new List<DigitalKey>()
    {
        new DigitalKey("8327-4782-2389-4847")
        {
            Name = "Kaspersky Antivirus",
            Description = "Антивирус Касперского - это базовое решение для защиты вашего компьютера от вирусов и других угроз. Он совместим со всеми устройствами и включает расширенные функции безопасности, такие как сетевой экран и защита от рекламного ПО1. Антивирусная защита в режиме реального времени обеспечивает надежную защиту вашего компьютера от вредоносных программ1. Антивирус Касперского - это проверенное и надежное решение, которому доверяют миллионы пользователей по всему миру.",
            Tag = Tag.Classic,
            FullPrice = 126,
            Discount = 0.10,
            Quantity = 10,
            Sold = 453,
            PictureUri = "b_01.jpg",
            CategoryId = 1
        },
        new DigitalKey("1234-5678-9012-3456")
        {
            Name = "ESET NOD32 Antivirus",
            Description = "ESET NOD32 Antivirus - это мощное антивирусное решение, которое обеспечивает защиту вашего компьютера от вирусов, троянов, червей, шпионских программ и других угроз. Он использует передовые технологии обнаружения и удаления вредоносных программ, чтобы обеспечить максимальную безопасность вашего компьютера.",
            Tag = Tag.Classic,
            FullPrice = 150,
            Discount = 0.15,
            Quantity = 20,
            Sold = 500,
            PictureUri = "b_02.jpg",
            CategoryId = 1
        },
        new DigitalKey("9876-5432-1098-7654")
        {
            Name = "Avast Antivirus",
            Description = "Avast Antivirus - это бесплатное антивирусное решение для домашних пользователей. Оно обеспечивает базовую защиту вашего компьютера от вирусов, червей, троянов и других угроз. Avast также включает в себя дополнительные функции безопасности, такие как сетевой экран и защита от фишинга.",
            Tag = Tag.Bestseller,
            FullPrice = 120,
            Discount = 0,
            Quantity = 100,
            Sold = 1000,
            PictureUri = "b_03.jpg",
            CategoryId = 1
        },
        new DigitalKey("4567-8901-2345-6789")
        {
            Name = "Norton Antivirus",
            Description = "Norton Antivirus - это популярное антивирусное решение, которое обеспечивает защиту вашего компьютера от вирусов, червей, троянов и других угроз. Оно использует передовые технологии обнаружения и удаления вредоносных программ, чтобы обеспечить максимальную безопасность вашего компьютера.",
            Tag = Tag.None,
            FullPrice = 20,
            Discount = 0.20,
            Quantity = 5,
            Sold = 200,
            PictureUri = "b_04.jpg",
            CategoryId = 1
        },
        new DigitalKey("1111-2222-3333-4444")
        {
            Name = "Microsoft Office",
            Description = "Microsoft Office - это пакет офисных приложений, который включает в себя текстовый процессор Word, электронные таблицы Excel, программу для создания презентаций PowerPoint и другие полезные инструменты. Он предлагает множество функций для создания и редактирования документов, таблиц и презентаций.",
            Tag = Tag.Bestseller,
            FullPrice = 100,
            Discount = 0.10,
            Quantity = 50,
            Sold = 1000,
            PictureUri = "b_05.jpg",
            CategoryId = 4
        },
        new DigitalKey("5555-6666-7777-8888")
        {
            Name = "Adobe Photoshop",
            Description = "Adobe Photoshop - это профессиональное программное обеспечение для редактирования изображений и создания графических работ. Оно предлагает множество инструментов и функций для обработки фотографий, создания цифровых иллюстраций и дизайна веб-страниц.",
            Tag = Tag.Bestseller,
            FullPrice = 200,
            Discount = 0.15,
            Quantity = 20,
            Sold = 500,
            PictureUri = "b_06.jpg",
            CategoryId = 4
        },
        new DigitalKey("9999-0000-1111-2222")
        {
            Name = "WinRAR",
            Description = "WinRAR - это популярное программное обеспечение для архивации и сжатия файлов. Оно позволяет создавать и распаковывать архивы в форматах RAR и ZIP, а также поддерживает множество других форматов архивации. WinRAR также предлагает множество дополнительных функций, таких как защита паролем и шифрование данных.",
            Tag = Tag.Classic,
            FullPrice = 30,
            Discount = 0.10,
            Quantity = 100,
            Sold = 2000,
            PictureUri = "b_07.jpg",
            CategoryId = 4
        },
        new DigitalKey("3333-4444-5555-6666")
        {
            Name = "VLC Media Player",
            Description = "VLC Media Player - это бесплатный медиаплеер с открытым исходным кодом, который поддерживает множество аудио и видео форматов. Он также предлагает множество дополнительных функций, таких как потоковое вещание, преобразование форматов и запись экрана.",
            Tag = Tag.Classic,
            FullPrice = 0,
            Discount = 0,
            Quantity = 1000,
            Sold = 5000,
            PictureUri = "b_08.jpg",
            CategoryId = 4
        }
    };
    }
};