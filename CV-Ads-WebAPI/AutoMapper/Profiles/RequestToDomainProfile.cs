using AutoMapper;
using CV_Ads_WebAPI.Contracts.DTOs.Request.AdvertisementCreation;
using CV_Ads_WebAPI.Domain.Models;
using System.IO;

namespace CV_Ads_WebAPI.AutoMapper.Profiles
{
    public class RequestToDomainProfile : Profile
    {
        public RequestToDomainProfile()
        {
            CreateMap<TimePeriodLimitRequest, TimePeriodLimit>();
            CreateMap<HumanLimitRequest, HumanLimit>();

            CreateMap<CreateAdvertisementRequest, Advertisement>()
                .AfterMap((dto, ad) => ad.PictureExtension = Path.GetExtension(dto.FormFile.FileName));
        }
    }
}
