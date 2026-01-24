public static class ClaimsPrincipalExtensions
{
    public static string GetUserId(this System.Security.Claims.ClaimsPrincipal user)
    {
        return user.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
               ?? throw new Exception("User ID not found in token");
    }
}