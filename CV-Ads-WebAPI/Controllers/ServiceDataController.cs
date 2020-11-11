using CV_Ads_WebAPI.Contracts;
using CV_Ads_WebAPI.Contracts.DTOs.Request;
using CV_Ads_WebAPI.Domain.Constants;
using CV_Ads_WebAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CV_Ads_WebAPI.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.ADMIN)]
    public class ServiceDataController : ControllerBase
    {
        private readonly ServiceDataService _serviceDataService;

        public ServiceDataController(ServiceDataService serviceDataService)
        {
            _serviceDataService = serviceDataService;
        }

        [HttpGet(ApiRoutes.ServiceData.Export)]
        public async Task<IActionResult> ExportServiceData()
        {
            byte[] fileContent = await _serviceDataService.ExportAsync();
            return File(fileContent, "text/plain");
        }

        [HttpPost(ApiRoutes.ServiceData.Import)]
        public async Task<IActionResult> ImportServiceData([FromForm]ImportServiceDataRequest importServiceDataRequest)
        {
            byte[] fileContentBytes = await ReadFileContentBytes(importServiceDataRequest);
            await _serviceDataService.ImportAsync(fileContentBytes);
            return Ok();
        }

        private async Task<byte[]> ReadFileContentBytes(ImportServiceDataRequest importServiceDataRequest)
        {
            byte[] fileContentBytes;
            using (var readStream = importServiceDataRequest.FormFile.OpenReadStream())
            {
                fileContentBytes = new byte[readStream.Length];
                await readStream.ReadAsync(fileContentBytes, 0, (int)readStream.Length);
            }

            return fileContentBytes;
        }
    }
}
