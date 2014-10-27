using System.Security.Principal;
using System.Threading;
using Bifrost.Security;

namespace Web
{
    public class PrincipalResolver : ICanResolvePrincipal
    {
        public IPrincipal Resolve()
        {
            return Thread.CurrentPrincipal;
        }
    }
}