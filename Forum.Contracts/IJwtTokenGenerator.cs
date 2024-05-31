using Forum.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace Forum.Contracts
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(IdentityUser applicationUser, IEnumerable<string> roles);
    }
}
