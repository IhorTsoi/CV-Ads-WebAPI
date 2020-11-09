using CV_Ads_WebAPI.Domain.Models;
using CV_Ads_WebAPI.Domain.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CV_Ads_WebAPI.Services
{
    public class JWTTokenService
    {
        private readonly JWTOptions _JWTOptions;

        public JWTTokenService(IOptions<JWTOptions> JWTOptions)
        {
            _JWTOptions = JWTOptions.Value;
        }

        public JwtSecurityToken CreateJWTToken(UserIdentity identity)
        {
            var now = DateTime.UtcNow;
            var JWTToken = new JwtSecurityToken(
                issuer: _JWTOptions.Issuer,
                audience: _JWTOptions.Audience,
                notBefore: now,
                claims: GetClaims(identity),
                expires: now.Add(TimeSpan.FromMinutes(_JWTOptions.LifeTime)),
                signingCredentials: new SigningCredentials(
                    key: _JWTOptions.GetSymmetricSecurityKey(),
                    algorithm: SecurityAlgorithms.HmacSha256)
            );
            return JWTToken;
        }

        public string EncodeJWTToken(JwtSecurityToken JWTToken) =>
            new JwtSecurityTokenHandler().WriteToken(JWTToken);

        private IEnumerable<Claim> GetClaims(UserIdentity userIdentity)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userIdentity.Id.ToString()),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, userIdentity.Role)
            };
            return claims;
        }

    }
}
