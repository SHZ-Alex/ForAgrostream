using AutoMapper;
using Company.Delivery.Api.Controllers.Waybills.Request;
using Company.Delivery.Api.Controllers.Waybills.Response;
using Company.Delivery.Core;
using Company.Delivery.Domain.Dto;
namespace Company.Delivery.Api;

/// <summary>
/// Автомаппер
/// </summary>
public class MappingConfig : Profile
{
    /// <summary>
    /// Маппер конфиг
    /// </summary>
    public MappingConfig()
    {
        CreateMap<WaybillCreateDto, WaybillDto>();
        CreateMap<WaybillCreateDto, Waybill>();
        CreateMap<CargoItemCreateRequest, CargoItemCreateDto>();
        CreateMap<WaybillDto, Waybill>().ReverseMap();

        CreateMap<WaybillResponse, WaybillDto>().ReverseMap();
        CreateMap<CargoItemDto, CargoItemResponse>().ReverseMap();

        CreateMap<WaybillUpdateRequest, WaybillUpdateDto>().ReverseMap();
        CreateMap<CargoItemUpdateDto, CargoItemUpdateRequest>().ReverseMap();
        CreateMap<CargoItemUpdateDto, CargoItem>().ReverseMap();
        CreateMap<WaybillUpdateDto, Waybill>().ReverseMap();

        CreateMap<WaybillCreateRequest, WaybillCreateDto>();
        CreateMap<CargoItemCreateDto, CargoItem>();
        CreateMap<CargoItem, CargoItemDto>();

    }
}

