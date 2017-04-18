using System.Security.Claims;
using Bifrost.Security;

namespace Web
{
    public class PrincipalResolver : ICanResolvePrincipal
    {
        public ClaimsPrincipal Resolve()
        {
            return ClaimsPrincipal.Current;
        }
    }
}