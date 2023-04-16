using System.Security.Claims;
using System.Threading.Tasks;
using Bravure.Constants;
using Bravure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Bravure.Infrastructure.Auth
{
    public class ClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, ApplicationRole>
    {
        public ClaimsPrincipalFactory(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IOptions<IdentityOptions> optionsAccessor
            )
            : base(userManager, roleManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim(CustomClaims.DisplayName, user.FullName));
            identity.AddClaim(new Claim(ClaimTypes.Email, user.UserName));

            return identity;
        }
    }
}
