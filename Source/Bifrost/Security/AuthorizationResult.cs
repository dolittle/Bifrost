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

        /// <summary>
        /// Gets any <see cref="AuthorizeActionResult"> results</see> that were not authorized
        /// </summary>
        public IEnumerable<AuthorizeActionResult> AuthorizeActionResults
        {
            get { return _authorizeActionResults.AsEnumerable(); }
        } 

        /// <summary>
        /// Gets the result of the Authorization attempt for this action and <see cref="ISecurityDescriptor"/>
        /// </summary>
        public bool IsAuthorized 
        {
            get { return _authorizeActionResults.All(r => r.IsAuthorized); }
        }

        /// <summary>
        /// Adds an instance of an <see cref="AuthorizeActionResult"/>
        /// </summary>
        /// <param name="result">Result to add</param>
        public void AddAuthorizeActionResult(AuthorizeActionResult result)
        {
            _authorizeActionResults.Add(result);
        }
    }
}