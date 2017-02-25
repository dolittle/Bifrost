using System.Security.Principal;
using System.Threading;
using System.Web;
using Bifrost.Security;

namespace Web
{
    public class PrincipalResolver : ICanResolvePrincipal
    {
        public IPrincipal Resolve()
        {
            return HttpContext.Current?.User ?? Thread.CurrentPrincipal;
        }
    }
}