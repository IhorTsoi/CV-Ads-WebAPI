using CV_Ads_WebAPI.Domain.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CV_Ads_WebAPI.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.SMART_DEVICE)]
    public class SmartDeviceHub : Hub
    {
        private static readonly HashSet<string> ConnectedUsersIds = new HashSet<string>();

        public override Task OnConnectedAsync()
        {
            ConnectedUsersIds.Add(Context.UserIdentifier);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            ConnectedUsersIds.Remove(Context.UserIdentifier);
            return base.OnDisconnectedAsync(exception);
        }

        public static bool IsSmartDeviceConnected(string smartDeviceId) => ConnectedUsersIds.Contains(smartDeviceId);
    }
}
