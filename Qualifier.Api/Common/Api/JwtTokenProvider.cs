using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Qualifier.Common.Api
{
    public class JwtTokenProvider
    {
        public static string GenerateToken(IConfiguration _configuration, int userId, string fullName, string currentRole, List<string> roles, int companyId, int standardId, string standardName)
        {
            string? secretKey = _configuration["Authentication:SecretKey"];

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);

            var claims = new[]
            {
                 new Claim("fn", fullName),
                 new Claim("cr", currentRole),
                 new Claim("userId", userId.ToString()),
                 new Claim("companyId", companyId.ToString()),
                 new Claim("standardId", standardId.ToString()),
                 new Claim("cs", standardName.ToString()),
                 new Claim("rls",  JsonConvert.SerializeObject(roles)),
            };

            var payload = new JwtPayload
            (
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claims,
                DateTime.Now,
                DateTime.UtcNow.AddDays(1)
            );

            var token = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static string GetCompanyIdFromToken(string token)
        {
            var TokenInfo = new Dictionary<string, string>();

            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
            var claims = jwtSecurityToken.Claims.ToList();

            foreach (var claim in claims)
            {
                TokenInfo.Add(claim.Type, claim.Value);
            }

            var companyId = TokenInfo["companyId"];

            return companyId;
        }

        public static string GetStandardIdFromToken(string token)
        {
            var TokenInfo = new Dictionary<string, string>();

            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
            var claims = jwtSecurityToken.Claims.ToList();

            foreach (var claim in claims)
            {
                TokenInfo.Add(claim.Type, claim.Value);
            }

            var companyId = TokenInfo["standardId"];

            return companyId;
        }

        public static string GetUserIdFromToken(string token)
        {
            var TokenInfo = new Dictionary<string, string>();

            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
            var claims = jwtSecurityToken.Claims.ToList();

            foreach (var claim in claims)
            {
                TokenInfo.Add(claim.Type, claim.Value);
            }

            var companyId = TokenInfo["userId"];

            return companyId;
        }

    }
}
