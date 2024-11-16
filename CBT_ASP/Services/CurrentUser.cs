using CBT.Application.Interfaces;
using System.Security.Claims;

namespace CBT.Web.Services
{
    public class CurrentUser(IHttpContextAccessor httpContextAccessor)
        : IUser
    {
        private readonly Lazy<Guid?> lazyId = new Lazy<Guid?>(() 
            => httpContextAccessor
                .HttpContext?
                .User?
                .FindFirst(ClaimTypes.NameIdentifier)?
                .Value is string id 
            && Guid.TryParse(id, out Guid guid)
                    ? guid
                    : null);

        public Guid? Id => lazyId.Value;
    }
}
