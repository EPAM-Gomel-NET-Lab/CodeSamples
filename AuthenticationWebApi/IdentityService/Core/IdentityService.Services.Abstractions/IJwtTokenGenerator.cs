using Microsoft.AspNetCore.Identity;

namespace IdentityService.Services.Abstractions
{
    public interface IJwtTokenGenerator
    {
        string GenerateJwtToken(IdentityUser user, string roleName);
    }
}
