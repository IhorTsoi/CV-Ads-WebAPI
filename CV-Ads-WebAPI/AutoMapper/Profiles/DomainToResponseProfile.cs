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
        }
    }
}
