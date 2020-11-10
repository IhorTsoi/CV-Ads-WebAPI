using CV_Ads_WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Threading.Tasks;

namespace CV_Ads_WebAPI.Services
{
    public class LocalFileStorageService : IFileStorageService
    {
        private const string ADVERTISEMENT_IMAGES_DIRECTORY = "AdvertisementImages";

        private readonly string _basePath;

        public LocalFileStorageService(IWebHostEnvironment environment)
        {
            _basePath = Path.Combine(environment.WebRootPath, ADVERTISEMENT_IMAGES_DIRECTORY);
        }

        public async Task SaveFileAsync(string filename, Stream uploadStream)
        {
            string fullFilePath = Path.Combine(_basePath, filename);
            using FileStream localFileStream = File.Create(fullFilePath);
            await uploadStream.CopyToAsync(localFileStream);
        }

        public string GetUrlForFile(string filename)
            => $"/{ADVERTISEMENT_IMAGES_DIRECTORY}/{filename}";
    }
}
