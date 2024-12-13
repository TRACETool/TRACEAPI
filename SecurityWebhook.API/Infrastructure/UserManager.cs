namespace SecurityWebhook.API.Infrastructure
{
    using System.Security.Principal;
    using System.Linq;
    using System.Security.Claims;

    public static class UserManager
    {
        /// <summary>
        /// Retrieves the specified claims from the user's claims.
        /// </summary>
        /// <param name="user">The principal object (User).</param>
        /// <returns>An anonymous object containing the UKI, AK, and UEM claims as strings.</returns>
        public static object GetData(this IPrincipal user)
        {
            if (user?.Identity is ClaimsIdentity claimsIdentity)
            {
                return new
                {
                    UKI = claimsIdentity.Claims.FirstOrDefault(c => c.Type == "UKI")?.Value,
                    AK = claimsIdentity.Claims.FirstOrDefault(c => c.Type == "AK")?.Value,
                    UEM = claimsIdentity.Claims.FirstOrDefault(c => c.Type == "UEM")?.Value
                };
            }
            return new
            {
                UKI = (string)null,
                AK = (string)null,
                UEM = (string)null
            };
        }

        public static string GetEmail(this IPrincipal user)
        {
            if (user?.Identity is ClaimsIdentity claimsIdentity)
            {
                var UEM = claimsIdentity.Claims.FirstOrDefault(c => c.Type == "UEM")?.Value;
                return UEM;

            }
            return null;
        }
        public static string GetUserId(this IPrincipal user)
        {
            if (user?.Identity is ClaimsIdentity claimsIdentity)
            {
                var UEM = claimsIdentity.Claims.FirstOrDefault(c => c.Type == "UKI")?.Value;
                return UEM;

            }
            return null;
        }

        public static string GetAPIToken(this IPrincipal user)
        {
            if (user?.Identity is ClaimsIdentity claimsIdentity)
            {
                var UEM = claimsIdentity.Claims.FirstOrDefault(c => c.Type == "AK")?.Value;
                return UEM;

            }
            return null;
        }
    }

}
