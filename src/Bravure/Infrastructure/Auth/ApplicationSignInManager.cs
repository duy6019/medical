using System.Threading.Tasks;
using Bravure.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Bravure.Infrastructure.Auth
{
    public class ApplicationSignInManager<TUser> : SignInManager<TUser> where TUser : ApplicationUser
    {
        private readonly UserManager<TUser> _userManager;

        public ApplicationSignInManager(UserManager<TUser> userManager,
            IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<TUser> claimsFactory,
            IOptions<IdentityOptions> optionsAccessor,
            ILogger<ApplicationSignInManager<TUser>> logger,
            IAuthenticationSchemeProvider schemes, IUserConfirmation<TUser> confirmation) :
            base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
        {
            _userManager = userManager;
        }

        public override async Task<SignInResult> PasswordSignInAsync(string userName, string password, bool rememberMe, bool shouldLockout)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return await Task.FromResult(SignInResult.Failed);
            }

            return await base.PasswordSignInAsync(userName, password, rememberMe, shouldLockout);
        }
    }
}
