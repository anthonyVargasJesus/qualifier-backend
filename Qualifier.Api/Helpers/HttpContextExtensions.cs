using Microsoft.AspNetCore.Authentication;
using Qualifier.Common.Api;

namespace Qualifier.Api.Helpers
{
    public static class HttpContextExtensions
    {
        public static int GetUserIdAsync(this HttpContext httpContext, string? accessToken)
        {
            string userIdString = string.Empty;

            if (!string.IsNullOrEmpty(accessToken))
                userIdString = JwtTokenProvider.GetUserIdFromToken(accessToken);
            
            if (int.TryParse(userIdString, out int userId))
                return userId;
            
            return 0; // O lanzar una excepción
        }

        public static int GetCompanyIdAsync(this HttpContext httpContext, string? accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
                return 0;

            var companyIdString = JwtTokenProvider.GetCompanyIdFromToken(accessToken);

            if (int.TryParse(companyIdString, out int companyId))
                return companyId;
            else
                return 0;
     
        }

    }
}
