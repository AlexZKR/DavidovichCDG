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
        .ForMember(dest => dest.KeyCategoryId, opt => opt.MapFrom(src => ((int)src.KeyCategoryId!)));
        CreateMap<ProductDTO, ProductViewModel>()
        .ForMember(dest => dest.KeyCategoryId, opt => opt.MapFrom(src => ((int)src.KeyCategoryId!)))
        .ForMember(dest => dest.TagDisplay, opt => opt.MapFrom(src => (EnumHelper<Tag>.GetDisplayValue((Tag)src.Tag!))))
        // mapping static lists data
        .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => (EnumHelper<Tag>.GetStaticDataFromEnum(Tag.None))));
    }

}
