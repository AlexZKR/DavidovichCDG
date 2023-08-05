using AutoMapper;
using CDG.BLL.Entities.BasketAggregate;
using CDG.BLL.Entities.Products;
using CDG.Web.Models.Basket;
using CDG.Web.Models.Catalog;

namespace CDG.Web.Configuration.MapperConfig;

public class ViewModelMapProfile : Profile
{
    public ViewModelMapProfile()
    {
        CreateMap<BasketItem, BasketItemViewModel>().ReverseMap();
        // .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice)).ReverseMap();
        CreateMap<CatalogItemViewModel, DigitalKey>().ReverseMap()
        .ForMember(dest => dest.KeyCategoryName, opt => opt.MapFrom(src => src.KeyCategory.Name))
        .ForPath(dest => dest.Rating.Rating, opt => opt.MapFrom(src => src.Rating));
    }
}