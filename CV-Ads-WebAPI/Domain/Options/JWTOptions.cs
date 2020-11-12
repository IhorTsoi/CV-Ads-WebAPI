using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CV_Ads_WebAPI.Domain.Options
{
    public class JWTOptions
    {
        public const string SectionName = "JWTOptions";

        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int LifeTime { get; set; }
        public string Key { get; set; }

        public SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
    }
}
