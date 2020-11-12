using AutoMapper;
using CV_Ads_WebAPI.AutoMapper.Resolvers;
using CV_Ads_WebAPI.Contracts.DTOs.RequestResponse;
using CV_Ads_WebAPI.Contracts.DTOs.Response;
using CV_Ads_WebAPI.Domain.Models;

namespace CV_Ads_WebAPI.AutoMapper.Profiles
{
    public class DomainToResponseProfile : Profile
    {
        public DomainToResponseProfile()
        {
            CreateMap<TimePeriodLimit, TimePeriodLimitDTO>();
            CreateMap<HumanLimit, HumanLimitDTO>();

            CreateMap<Advertisement, AdvertisementResponse>()
                .ForMember(response => response.PictureLink, action => action.MapFrom<PictureLinkResolver>())
                .AfterMap((ad, response) => response.ViewsCount = ad.AdvertisementViews.Count);

            CreateMap<SmartDevice, SmartDevicePartnerResponse>()
                .BeforeMap((sd, sdDTO) => sdDTO.SerialNumber = sd.UserIdentity.Login);
            CreateMap<SmartDevice, SmartDeviceAdminResponse>()
                .BeforeMap((sd, sdDTO) => sdDTO.SerialNumber = sd.UserIdentity.Login)
                .BeforeMap((sd, sdDTO) => sdDTO.PartnerEmail = sd.Partner.UserIdentity.Login);

            CreateMap<AdvertisementView, AdvertisementViewDTO>();
        }
    }
}
