using AutoMapper;
using CDG.BLL.Entities.Enums;
using CDG.BLL.Entities.Order;
using CDG.BLL.Entities.Products;
using CDG.Web.Infrastructure;
using CDG.Web.Models.DigitalKey;
using CDG.Web.Models.DTOs.Order;
using CDG.Web.Models.KeyCategory;

namespace CDG.Web.Configuration.MapperConfig;

public class DTOMapProfile : Profile
{
    public DTOMapProfile()
    {
        CreateMap<OrderDTO, Order>().ReverseMap()
            .ForMember(dest => dest.BuyerId, opt => opt.MapFrom(src => src.Buyer.BuyerId))
            .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderInfo.OrderDate))
            .ForMember(dest => dest.BuyerName, opt => opt.MapFrom(src => (src.Buyer.FirstName + " " + src.Buyer.LastName)))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Buyer.PhoneNumber))
            .ForMember(dest => dest.IsInProcess, opt => opt.MapFrom(src => src.IsInProcess));

        CreateMap<OrderItemDTO, OrderItem>().ReverseMap();

        CreateMap<BuyerDTO, Buyer>().ReverseMap()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.BuyerId))
        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => (src.FirstName + " " + src.LastName)));

        CreateMap<KeyCategoryDTO, KeyCategory>().ReverseMap();

        CreateMap<DigitalKeyDTO, DigitalKey>().ReverseMap()
            .ForMember(dest => dest.Tag, opt => opt.MapFrom(src => ((int)src.Tag)));
    }
}