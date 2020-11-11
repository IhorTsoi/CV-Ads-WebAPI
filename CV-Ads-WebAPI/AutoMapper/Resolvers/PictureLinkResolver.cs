using AutoMapper;
using CV_Ads_WebAPI.Contracts.DTOs.Response;
using CV_Ads_WebAPI.Domain.Models;
using CV_Ads_WebAPI.Services.Interfaces;

namespace CV_Ads_WebAPI.AutoMapper.Resolvers
{
    public class PictureLinkResolver : IValueResolver<Advertisement, AdvertisementResponse, string>
    {
        private readonly IFileStorageService _fileStorageService;

        public PictureLinkResolver(IFileStorageService fileStorageService)
        {
            _fileStorageService = fileStorageService;
        }

        public string Resolve(Advertisement source, AdvertisementResponse _, string __, ResolutionContext ___) =>
            _fileStorageService.GetUrlForFile(source.GetFileName());
    }
}
