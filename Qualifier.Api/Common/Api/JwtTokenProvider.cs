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
        // standardId/cs (nombre de la norma) se sacaron del token a propósito: eran la norma
        // "fija" del usuario al momento de loguearse, y quedaban desincronizados apenas la
        // evaluación actual pasaba a ser de otra norma (ver el bug que corrigió esto en
        // GetPlanDeAccionBootstrapQuery, y el mismo patrón que hacía que "Nueva evaluación"
        // ignorara la norma elegida en el formulario y usara la del token -- ver
        // EvaluationController.Create). Cualquier lugar que necesite la norma correcta debe
        // resolverla de la evaluación actual o de la entidad puntual que esté consultando,
        // nunca del token.
        public static string GenerateToken(IConfiguration _configuration, int userId, string fullName, string currentRole, List<string> roles, int companyId, string email = "")
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
                 new Claim("rls",  JsonConvert.SerializeObject(roles)),
                 // LoginModel.em (Angular) ya esperaba este claim -- nunca se mandó, por eso
                 // pantallas como "Mis asignaciones" mostraban el email vacío ("Acciones
                 // asignadas a .").
                 new Claim("em", email ?? ""),
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
