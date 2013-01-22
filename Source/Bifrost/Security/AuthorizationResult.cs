using System.Collections.Generic;
using System.Linq;

namespace Bifrost.Security
{
    /// <summary>
    /// Represents the result of an authorization attempt
    /// </summary>
    public class AuthorizationResult
    {
        readonly List<AuthorizeActionResult> _authorizeActionResults = new List<AuthorizeActionResult>();

        public IEnumerable<AuthorizeActionResult> AuthorizeActionResults
        {
            get { return _authorizeActionResults.AsEnumerable(); }
        } 

        public bool IsAuthorized 
        {
            get { return _authorizeActionResults.All(r => r.IsAuthorized); }
        }

        public void AddAuthorizeActionResult(AuthorizeActionResult result)
        {
            _authorizeActionResults.Add(result);
        }
    }
}