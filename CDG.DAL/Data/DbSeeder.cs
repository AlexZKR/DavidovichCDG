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
                // await context.KeyCategorys.AddRangeAsync(
                //     GetPreconfiguredKeyCategorys());

                // await context.SaveChangesAsync();
            }
            if (!await context.DigitalKeys.AnyAsync())
            {
                // await context.DigitalKeys.AddRangeAsync(
                //     GetPreconfiguredDigitalKeys());

                // await context.SaveChangesAsync();
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
                Name = "Олдос Хаксли",
                Description = "Олдос Хаксли — английский писатель, больше всего известный благодаря роману «О дивный новый мир».",
            },
            new KeyCategory
            {
                Name = "Рэй Брэдберри",
                Description = "Многим этот американский писатель известен по антиутопии «451 градус по Фаренгейту», циклу рассказов «Марсианские хроники» и частично автобиографическому роману «Вино из одуванчиков».  Но за свою жизнь Брэдбери создал более восьмисот разных литературных произведений, в том числе несколько романов и повестей, сотни рассказов, десятки пьес, ряд статей, заметок и стихотворений. Его истории легли в основу нескольких экранизаций, театральных постановок и музыкальных сочинений. Брэдбери традиционно считается классиком научной фантастики, хотя значительная часть его творчества тяготеет к жанру фэнтези, притчи или сказки. Пьесы Брэдбери были хорошо приняты публикой, но его стихотворения не пользовались большим успехом.",
            },
            new KeyCategory
            {
                Name = "Юваль Ной Харари",
                Description = "Учился в Еврейском университете, изучал военную и средневековую историю, защитил докторскую в Оксфордском колледже Иисуса, два года занимался постдокторантурой. Свои научно-популярные работы начал издавать с 2004-го. Книги Юваля Ноя Харари пользуются огромной популярностью у читателей, которые интересуются историей человечества и стремятся сделать системные выводы о разных аспектах существования своего вида.",
            },
            new KeyCategory
            {
                Name = "Ольга Примаченко",
                Description = "белорусская журналистка, психолог, блогер, автор вдохновляющих книг о самопознании и гармоничных отношениях с собой и окружающими.",
            },
            new KeyCategory
            {
                Name = "Эрих Мария Ремарк",
                Description = "Эрих Мария Ремарк — немецкий прозаик, один из ярчайших представителей «потерянного поколения» — молодых людей, которые после ужасов Первой мировой увидели послевоенный мир совсем не таким, каким он представлялся в период войны.",
            },
            new KeyCategory
            {
                Name = "Братья Стругацкие",
                Description = "Практически все их произведения стали классикой советской фантастики. Наиболее известны \"Трудно быть богом\", \"Понедельник начинается в субботу\", \"Пикник на обочине\", \"Жук в муравейнике\", \"Улитка на склоне\", \"Малыш\".",
            },


        };
    }

    private static IEnumerable<DigitalKey> GetPreconfiguredDigitalKeys()
    {
        return new List<DigitalKey>()
        {
            new DigitalKey
                    {
                        Name = "О дивный новый мир",
                        Description = "Один из самых знаменитых романов-антиутопий. Своего рода антипод \"1984\" Оруэлла. Никаких пыточных застенков – все счастливы и довольны. Люди выращиваются на заводах-эмбрионариумах и заранее (воздействием на эмбрион) поделены на пять различных по умственным и физическим способностям каст, которые выполняют разную работу. От \"альф\" – крепких и красивых интеллектуалов, существующих в единственном экземпляре, до \"эпсилонов\" – полукретинов, которым доступна только самая простая физическая работа, клонируемых пачками... Всеобщему счастью способствует воспитание младенцев по Павлову, \"free love\" и не имеющий побочных эффектов лёгкий наркотик \"сома\". Всё это – непреложные добродетели, закреплённые тысячами рекламных слоганов...",

                        Language = Language.Russian,

                        Tag = Tag.Classic,

                        FullPrice = 12,
                        Discount = 0.10,
                        Quantity = 10,

                        Sold = 453,

                        PictureUri = "b_01.jpg",
                        CategoryId = 1

                    },
                    new DigitalKey
                    {
                        Name = "Остров",
                        Description = "Идеи культового \"О дивного нового мира\" нашли продолжение в последнем, самом загадочном и мистическом романе Олдоса Хаксли \"Остров\". Задуманное автором как антиутопия, это произведение оказалось гораздо масштабнее узких рамок утопического жанра. Этот подлинно великий философский роман – отражение современного общества.",

                        Language = Language.Russian,

                        Tag = Tag.None,

                        FullPrice = 8,
                        Discount = 0,
                        Quantity = 15,

                        Sold = 34,

                        PictureUri = "b_02.jpg",
                        CategoryId = 2

                    },
                    new DigitalKey
                    {
                        Name = "451 градус по Фаренгейту",
                        Description = "Пожарные, которые разжигают пожары, книги, которые запрещено читать, и люди, которые уже почти перестали быть людьми... Роман Рэя Брэдбери \"451 по Фаренгейту\" – это классика научной фантастики, ставшая классикой мирового кинематографа в воплощении знаменитого французского режиссера Франсуа Трюффо. А на очереди – экранизация этой книги прекрасным актером и режиссером Мелом Гибсоном.",


                        Language = Language.Russian,

                        Tag = Tag.Bestseller,

                        FullPrice = 15,
                        Discount = 0.15,
                        Quantity = 20,

                        Sold = 200,

                        PictureUri = "b_03.jpg",
                        CategoryId = 3
                    },
                    new DigitalKey
                    {
                        Name = "Вино из одуванчиков",
                        Description = "Войдите в светлый мир двенадцатилетнего мальчика и проживите вместе с ним одно лето, наполненное событиями радостными, печальными, загадочными и тревожными; лето, когда каждый день совершаются удивительные открытия, главное из которых – ты живой, ты дышишь, ты чувствуешь! \"Вино из одуванчиков\" Рэя Брэдбери – классическое произведение из золотого фонда мировой литературы. Это одна из книг, которые хочется перечитывать вновь и вновь.",


                        Language = Language.Russian,

                        Tag = Tag.Classic,

                        FullPrice = 10,
                        Discount = 0,
                        Quantity = 23,

                        Sold = 145,

                        PictureUri = "b_04.jpg",
                        CategoryId = 3
                    },
                    new DigitalKey
                    {
                        Name = "Sapiens: Краткая история человечества",
                        Description = "Книга профессора Юваля Ноя Харари, впервые опубликованная на иврите в Израиле в 2011 году, а на английском языке в 2014 году.",


                        Language = Language.English,

                        Tag = Tag.Bestseller,

                        FullPrice = 30,
                        Discount = 0.14,
                        Quantity = 45,

                        Sold = 950,

                        PictureUri = "b_05.jpg",
                        CategoryId = 4
                    },
                    new DigitalKey
                    {
                        Name = "К себе нежно. Книга о том, как ценить и беречь себя",
                        Description = "\"К себе нежно\" – это новый, очень честный взгляд на любовь к себе. Это книга-медитация, которая призывает к внутреннему разговору и помогает услышать собственный голос среди множества других.",


                        Language = Language.Belarusian,

                        Tag = Tag.None,

                        FullPrice = 25,
                        Discount = 0.05,
                        Quantity = 70,

                        Sold = 120,

                        PictureUri = "b_06.jpg",
                        CategoryId = 4
                    },
                    new DigitalKey
                    {
                        Name = "Триумфальная арка",
                        Description = "Триумфальная арка пронзительная история любви всему наперекор, любви, приносящей боль, но и дарующей бесконечную радость.",


                        Language = Language.Russian,

                        Tag = Tag.Bestseller,

                        FullPrice = 20,
                        Discount = 0.1,
                        Quantity = 150,

                        Sold = 120,

                        PictureUri = "b_07.jpg",
                        CategoryId = 5
                    },
                    new DigitalKey
                    {
                        Name = "Жизнь взаймы",
                        Description = "Жизнь взаймы это жизнь, которую герои отвоевывают у смерти. Когда терять уже нечего, когда один стоит на краю гибели, так эту жизнь и не узнав, а другому эта треклятая жизнь стала невыносима.",


                        Language = Language.Russian,

                        Tag = Tag.None,

                        FullPrice = 15,
                        Discount = 0.2,
                        Quantity = 70,

                        Sold = 120,

                        PictureUri = "b_08.jpg",
                        CategoryId = 5
                    },
                    new DigitalKey
                    {
                        Name = "Три товарища",
                        Description = "В биографии одного из самых известных немецких писателей Эриха Марии Ремарка значатся обучение в католической семинарии, участие в Первой мировой войне, работа продавцом надгробий, бессчетное количество интрижек и написание книг, которые навсегда изменили его жизнь и стали мировой классикой",


                        Language = Language.Russian,

                        Tag = Tag.Bestseller,

                        FullPrice = 30,
                        Discount = 0.0,
                        Quantity = 70,

                        Sold = 120,

                        PictureUri = "b_09.jpg",
                        CategoryId = 5
                    },
                    new DigitalKey
                    {
                        Name = "На Западном фронте без перемен",
                        Description = "Говоря о Первой мировой войне, всегда вспоминают одно произведение Эриха Марии Ремарка.На Западном фронте без перемен.",


                        Language = Language.Russian,

                        Tag = Tag.None,

                        FullPrice = 19,
                        Discount = 0.3,
                        Quantity = 70,

                        Sold = 250,

                        PictureUri = "b_10.jpg",
                        CategoryId = 5
                    },
                    new DigitalKey
                    {
                        Name = "На обратном пути",
                        Description = "Ах, как трудно прощаться! Но возвращаться иногда еще труднееСпустя четыре тяжелых года война, наконец, закончилась Эрнст и его фронтовые товарищи возвращаются домой в город, который некогда покинули еще детьми",


                        Language = Language.Russian,

                        Tag = Tag.Classic,

                        FullPrice = 15,
                        Discount = 0.03,
                        Quantity = 70,

                        Sold = 120,

                        PictureUri = "b_11.jpg",
                        CategoryId = 5
                    },
                    new DigitalKey
                    {
                        Name = "Пикник на обочине",
                        Description = "Пожалуй, в истории современной мировой фантастики найдется не так много произведений, которые оставались бы популярными столь долгое время.",


                        Language = Language.Russian,

                        Tag = Tag.Bestseller,

                        FullPrice = 12,
                        Discount = 0.2,
                        Quantity = 200,

                        Sold = 120,

                        PictureUri = "b_12.jpg",
                        CategoryId = 6
                    },
                    new DigitalKey
                    {
                        Name = "Понедельник начинается в субботу",
                        Description = "Понедельник начинается в субботу. Сказка для научных работников младшего возраста под таким заголовком в 1965 году вышла книга, которой зачитывались и продолжают зачитываться все новые и новые поколения.",


                        Language = Language.Russian,

                        Tag = Tag.None,

                        FullPrice = 25,
                        Discount = 0.05,
                        Quantity = 70,

                        Sold = 250,

                        PictureUri = "b_13.jpg",
                        CategoryId = 6
                    },
                    new DigitalKey
                    {
                        Name = "Трудно быть богом",
                        Description = "Трудно быть богом.Возможно, самое известное из произведений братьев Стругацких. Один из самых прославленных повестей отечественной фантастики.",


                        Language = Language.Russian,

                        Tag = Tag.None,

                        FullPrice = 13,
                        Discount = 0.05,
                        Quantity = 70,

                        Sold = 120,

                        PictureUri = "b_14.jpg",
                        CategoryId = 6
                    },
                    new DigitalKey
                    {
                        Name = "Улитка на склоне",
                        Description = "Одно из самых загадочных и провокационных произведений братьев Стругацких, печатавшееся по частям в 1966 и 1968 годах, после чего попало под запрет и пришло к читателю в полном объеме только в 1988 году.",


                        Language = Language.Russian,

                        Tag = Tag.None,

                        FullPrice = 16,
                        Discount = 0.05,
                        Quantity = 70,

                        Sold = 120,

                        PictureUri = "b_15.jpg",
                        CategoryId = 6
                    },
                    new DigitalKey
                    {
                        Name = "Забытый эксперимент",
                        Description = "В сборнике представлены ранние произведения братьев Стругацких рассказы и маленькие повести, относящиеся к разным жанрам, стилям и направлениям фантастики, от классической НФ до философской притчи.",


                        Language = Language.Russian,

                        Tag = Tag.None,

                        FullPrice = 8,
                        Discount = 0.05,
                        Quantity = 70,

                        Sold = 50,

                        PictureUri = "b_16.jpg",
                        CategoryId = 6
                    },
        };
    }
}
