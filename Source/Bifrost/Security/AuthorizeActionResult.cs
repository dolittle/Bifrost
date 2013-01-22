using System.Collections.Generic;
using System.Linq;

namespace Bifrost.Security
{
    /// <summary>
    /// Represents the result of an authorization of a <see cref="ISecurityAction"/>
    /// </summary>
    public class AuthorizeActionResult
    {
        readonly List<AuthorizeTargetResult> _authorizeTargetResults = new List<AuthorizeTargetResult>();

        public AuthorizeActionResult(ISecurityAction action)
        {
            Action = action;
        }

        public ISecurityAction Action { get; private set; }

        public IEnumerable<AuthorizeTargetResult> AuthorizeTargetResults
        {
            get { return _authorizeTargetResults.AsEnumerable(); }
        }
        
        
        public void AddAuthorizeTargetResult(AuthorizeTargetResult resultToAdd)
        {
            _authorizeTargetResults.Add(resultToAdd);
        }

        public bool IsAuthorized
        {
            get { return AuthorizeTargetResults.All(r => r.IsAuthorized); }
        }
    }
}