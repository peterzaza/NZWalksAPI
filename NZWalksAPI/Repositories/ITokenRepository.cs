using Microsoft.AspNetCore.Identity;

namespace NZWalksAPI.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWLToken(IdentityUser user,List<string> roles);
    }
}
