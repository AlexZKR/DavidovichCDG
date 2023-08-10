using AutoMapper;
using CDG.Admin.Infrastructure;
using CDG.Admin.Models.Order;
using CDG.Admin.Models.Product;
using CDG.Admin.ViewModels.Catalog;
using CDG.Admin.ViewModels.Order;

namespace CDG.Admin.Configuration.MapperConfig;

public class DTOMapProfile : Profile
{
    public DTOMapProfile()
    {
        CreateMap<OrderDTO, OrderViewModel>().ReverseMap();
        CreateMap<OrderItemDTO, OrderItemViewModel>().ReverseMap();
        CreateMap<BuyerDTO, BuyerViewModel>().ReverseMap();

        CreateMap<ProductViewModel, ProductDTO>()
        .ForMember(dest => dest.Cover, opt => opt.MapFrom(src => ((int)src.Cover!)))
        .ForMember(dest => dest.Language, opt => opt.MapFrom(src => ((int)src.Lang!)))
        .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => ((int)src.AuthorId!)))
        .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((int)src.Genre!)));
        CreateMap<ProductDTO, ProductViewModel>()
        .ForMember(dest => dest.Cover, opt => opt.MapFrom(src => ((int)src.Cover!)))
        .ForMember(dest => dest.Lang, opt => opt.MapFrom(src => ((int)src.Language!)))
        .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => ((int)src.AuthorId!)))
        .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((int)src.Genre!)))
        .ForMember(dest => dest.GenreDisplay, opt => opt.MapFrom(src => (EnumHelper<Genre>.GetDisplayValue((Genre)src.Genre!))))
        .ForMember(dest => dest.CoverDisplay, opt => opt.MapFrom(src => (EnumHelper<Cover>.GetDisplayValue((Cover)src.Cover!))))
        .ForMember(dest => dest.LanguageDisplay, opt => opt.MapFrom(src => (EnumHelper<Language>.GetDisplayValue((Language)src.Language!))))
        .ForMember(dest => dest.TagDisplay, opt => opt.MapFrom(src => (EnumHelper<Tag>.GetDisplayValue((Tag)src.Tag!))))
        // mapping static lists data
        .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => (EnumHelper<Genre>.GetStaticDataFromEnum(Genre.Business))))
        .ForMember(dest => dest.Covers, opt => opt.MapFrom(src => (EnumHelper<Cover>.GetStaticDataFromEnum(Cover.HardCover))))
        .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => (EnumHelper<Tag>.GetStaticDataFromEnum(Tag.None))))
        .ForMember(dest => dest.Languages, opt => opt.MapFrom(src => (EnumHelper<Language>.GetStaticDataFromEnum(Language.Russian))));
    }

}
