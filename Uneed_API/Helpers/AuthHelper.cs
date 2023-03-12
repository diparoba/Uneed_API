using System.Security.Claims;
using Microsoft.AspNetCore.Http;

public static class AuthHelper
{
    public static int GetUserId(HttpContext httpContext)
    {
        var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
        {
            throw new ApplicationException("User Id not found in claims.");
        }
        return userId;
    }
}
