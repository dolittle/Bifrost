using System.Collections.Generic;
using System.Linq;

namespace Bifrost.Security
{
    /// <summary>
    /// Represents the result of an authorization of a <see cref="ISecurityTarget"/>
    /// </summary>
    public class AuthorizeTargetResult
    {
        readonly List<AuthorizeSecurableResult> _securableResults = new List<AuthorizeSecurableResult>(); 

        /// <summary>
        /// Instantiates an instance of <see cref="AuthorizeTargetResult"/> for the specificed <see cref="ISecurityTarget"/>
        /// </summary>
        /// <param name="target"><see cref="ISecurityTarget"/> that this <see cref="AuthorizeTargetResult"/> pertains to.</param>
        public AuthorizeTargetResult(ISecurityTarget target)
        {
            Target = target;
        }

        /// <summary>
        /// Gets the <see cref="ISecurityTarget"/> that this <see cref="AuthorizeTargetResult"/> pertains to.
        /// </summary>
        public ISecurityTarget Target { get; private set; }

        /// <summary>
        /// Gets the <see cref="AuthorizeSecurableResult"/> for each <see cref="ISecurable"/> on the <see cref="ISecurityTarget"/>
        /// </summary>
        public IEnumerable<AuthorizeSecurableResult> AuthorizeSecurableResults
        {
            get { return _securableResults.AsEnumerable(); }
        }
        
        /// <summary>
        /// Indicates if the action instance has been authorized by the <see cref="ISecurityTarget"/>
        /// </summary>
        public bool IsAuthorized
        {
            get { return AuthorizeSecurableResults.All(r => r.IsAuthorized); }
        }

        /// <summary>
        /// Adds an <see cref="AuthorizeSecurableResult"/> to the collection of results
        /// </summary>
        /// <param name="result">An <see cref="AuthorizeSecurableResult"/> for a <see cref="ISecurable"/></param>
        public void AddAuthorizeSecurableResult(AuthorizeSecurableResult result)
        {
            _securableResults.Add(result);
        }
    }
}