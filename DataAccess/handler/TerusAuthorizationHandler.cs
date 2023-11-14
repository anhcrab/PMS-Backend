using System.Security.Claims;

namespace DataAccess.handler
{
    public class TerusAuthorizationHandler
    {
        public static string NormalizedToken(string rawToken)
        {
            var result = rawToken.Replace("Bearer", "");
            return result.Trim();
        }

    }
}
