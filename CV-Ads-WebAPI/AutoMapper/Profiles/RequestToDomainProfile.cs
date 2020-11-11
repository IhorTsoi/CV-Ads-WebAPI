using AutoMapper;
using CV_Ads_WebAPI.Contracts.DTOs.Request;
using CV_Ads_WebAPI.Contracts.DTOs.RequestResponse;
using CV_Ads_WebAPI.Domain.Models;
using System.IO;

namespace CV_Ads_WebAPI.AutoMapper.Profiles
{
    public class RequestToDomainProfile : Profile
    {
        public RequestToDomainProfile()
        {
            CreateMap<TimePeriodLimitDTO, TimePeriodLimit>();
            CreateMap<HumanLimitDTO, HumanLimit>();

            CreateMap<CreateAdvertisementRequest, Advertisement>()
                .AfterMap((dto, ad) => ad.PictureExtension = Path.GetExtension(dto.FormFile.FileName));
        }
    }
}
