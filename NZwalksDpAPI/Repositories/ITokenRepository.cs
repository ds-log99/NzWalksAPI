using Microsoft.AspNetCore.Identity;

namespace NZwalksDpAPI.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
