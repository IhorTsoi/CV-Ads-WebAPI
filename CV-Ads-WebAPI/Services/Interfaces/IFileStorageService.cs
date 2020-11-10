using System.IO;
using System.Threading.Tasks;

namespace CV_Ads_WebAPI.Services.Interfaces
{
    public interface IFileStorageService
    {
        public Task SaveFileAsync(string filename, Stream uploadStream);
        public string GetUrlForFile(string filename);
    }
}
