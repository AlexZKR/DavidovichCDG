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
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Buyer.PhoneNumber))
            .ForMember(dest => dest.PaymentType, opt => opt.MapFrom(src => (EnumHelper<PaymentType>.GetName(src.OrderInfo.PaymentType))))
            .ForMember(dest => dest.OrderComment, opt => opt.MapFrom(src => src.OrderInfo.OrderComment))
            .ForMember(dest => dest.IsInProcess, opt => opt.MapFrom(src => src.IsInProcess));

        CreateMap<OrderItemDTO, OrderItem>().ReverseMap();

        CreateMap<BuyerDTO, Buyer>().ReverseMap()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.BuyerId))
        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => (src.FirstName + " " + src.LastName)));

        CreateMap<KeyCategoryDTO, KeyCategory>().ReverseMap();

        CreateMap<DigitalKeyDTO, DigitalKey>().ReverseMap()
            .ForMember(dest => dest.Language, opt => opt.MapFrom(src => ((int)src.Language)))
            .ForMember(dest => dest.Tag, opt => opt.MapFrom(src => ((int)src.Tag)));
// CreateMap<DigitalKeyDTO, DigitalKey>()
//         .ForMember(dest => dest.Cover, opt => opt.MapFrom(src => ((int)src.Cover!).ToString()))
//         .ForMember(dest => dest.Language, opt => opt.MapFrom(src => ((int)src.Lang!).ToString()))
//         .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((int)src.Genre!).ToString()));
//         CreateMap<DigitalKey, DigitalKeyDTO>()
//         .ForMember(dest => dest.Cover, opt => opt.MapFrom(src => (EnumHelper<Cover>.GetEnumValueFromString(src.Cover!))))
//         .ForMember(dest => dest.Language, opt => opt.MapFrom(src => (EnumHelper<Language>.GetEnumValueFromString(src.Language!))))
//         .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => (EnumHelper<Genre>.GetEnumValueFromString(src.Genre!))))
    }
}