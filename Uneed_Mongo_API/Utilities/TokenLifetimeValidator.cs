using Microsoft.IdentityModel.Tokens;

namespace Uneed_Mongo_API.Utilities
{
    public class TokenLifetimeValidator
    {
        public static bool Validate(
            DateTime? notBefore,
            DateTime? expires,
            SecurityToken tokenToValidate,
            TokenValidationParameters @param
            )
        {
            return (expires != null && expires > DateTime.UtcNow);
        }
    }
}
