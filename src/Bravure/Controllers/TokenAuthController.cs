using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Bravure.Entities;
using Bravure.Exceptions;
using Bravure.Infrastructure.Auth;
using Bravure.JwtBearer;
using Bravure.Models.TokenAuth;
using Bravure.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bravure.Controllers
{
    [Route("api/[controller]/[action]")]
    public class TokenAuthController : ControllerBase
    {
        private readonly TokenAuthConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ClaimsPrincipalFactory _claimsPrincipalFactory;

        public TokenAuthController(
            TokenAuthConfiguration configuration,
            UserManager<ApplicationUser> userManager,
            ClaimsPrincipalFactory claimsPrincipalFactory
            )
        {
            _configuration = configuration;
            _userManager = userManager;
            _claimsPrincipalFactory = claimsPrincipalFactory;
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateModel model)
        {
            try
            {
                var loginResult = await GetLoginResultAsync(
                    model.UserNameOrEmailAddress,
                    model.Password
                );

                var accessToken = CreateAccessToken(CreateJwtClaims(loginResult.Item1));

                return Ok(new AuthenticateResultModel
                {
                    AccessToken = accessToken,
                    EncryptedAccessToken = GetEncryptedAccessToken(accessToken),
                    ExpireInSeconds = (int)_configuration.Expiration.TotalSeconds,
                    UserId = loginResult.Item2.Id
                });
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private async Task<(ClaimsIdentity, ApplicationUser)> GetLoginResultAsync(string usernameOrEmailAddress, string password)
        {
            var user = await _userManager.FindByNameAsync(usernameOrEmailAddress);
            if (user == null)
            {
                throw new BadRequestException("Tài khoản hoặc mật khẩu không đúng!");
            }
            if (!await _userManager.CheckPasswordAsync(user, password))
            {
                throw new BadRequestException("Tài khoản hoặc mật khẩu không đúng!");
            }
            var claims = await _claimsPrincipalFactory.CreateAsync(user);

            return (claims.Identity as ClaimsIdentity, user);
        }

        private string CreateAccessToken(IEnumerable<Claim> claims, TimeSpan? expiration = null)
        {
            var now = DateTime.UtcNow;

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration.Issuer,
                audience: _configuration.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(expiration ?? _configuration.Expiration),
                signingCredentials: _configuration.SigningCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

        private static List<Claim> CreateJwtClaims(ClaimsIdentity identity)
        {
            var claims = identity.Claims.ToList();
            var nameIdClaim = claims.First(c => c.Type == ClaimTypes.NameIdentifier);

            claims.AddRange(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, nameIdClaim.Value),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.Now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            });

            return claims;
        }

        private string GetEncryptedAccessToken(string accessToken)
        {
            return SimpleStringCipher.Instance.Encrypt(accessToken);
        }
    }
}
