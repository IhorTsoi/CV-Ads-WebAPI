using CV_Ads_WebAPI.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace CV_Ads_WebAPI.Services
{
    public class SmartDeviceHubService
    {
        private const string UPDATE_COMAND = "Update";
        private const string ACTIVATE_COMMAND = "Activate";

        private readonly IHubContext<SmartDeviceHub> _smartDeviceHub;

        public SmartDeviceHubService(IHubContext<SmartDeviceHub> smartDeviceHub)
        {
            _smartDeviceHub = smartDeviceHub;
        }

        public async Task SendUpdateMessageAsync(Guid smartDeviceId)
        {
            string recipientId = smartDeviceId.ToString();
            var user = _smartDeviceHub.Clients.User(recipientId);
            await user.SendAsync(UPDATE_COMAND);
        }

        public async Task<bool> TrySendActivateMessageAsync(Guid smartDeviceId, string newPassword)
        {
            string recipientId = smartDeviceId.ToString();
            if (SmartDeviceHub.IsSmartDeviceConnected(recipientId))
            {
                var user = _smartDeviceHub.Clients.User(recipientId);
                await user.SendAsync(ACTIVATE_COMMAND, newPassword);
                return true;
            }
            return false;
        }
    }
}
