using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Bravure.Constants;
using Microsoft.AspNetCore.Http;

namespace Bravure.Infrastructure.Services
{
    public interface IIdentityService
    {
        ClaimsPrincipal User { get; }
        Guid? CurrentUserId { get; }
        public IEnumerable<string> Roles { get; }
    }

    public class IdentityService : IIdentityService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IdentityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new NullReferenceException("IHttpContextAccessor is not accessible in the current context");
        }

        public ClaimsPrincipal User => _httpContextAccessor.HttpContext?.User;

        public Guid? CurrentUserId => IsAuthenticated ? Guid.Parse(_httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier)) : null;


        public IEnumerable<string> Roles => _httpContextAccessor.HttpContext?.User?.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value).ToList();

        public bool IsAuthenticated => CurrentUserId != null;
    }
}
