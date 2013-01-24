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

        /// <summary>
        /// Instantiates an instance of <see cref="AuthorizeActionResult"/> for the specificed <see cref="ISecurityAction"/>
        /// </summary>
        /// <param name="action"><see cref="ISecurityAction"/> that this <see cref="AuthorizeActionResult"/> pertains to.</param>
        public AuthorizeActionResult(ISecurityAction action)
        {
            Action = action;
        }

        /// <summary>
        /// Gets the <see cref="ISecurityAction"/> that this <see cref="AuthorizeTargetResult"/> pertains to.
        /// </summary>
        public ISecurityAction Action { get; private set; }

        /// <summary>
        /// Gets the <see cref="AuthorizeTargetResult"/> for all <see cref="ISecurityTarget"> Actors </see> on the <see cref="ISecurable"/>
        /// </summary>
        public IEnumerable<AuthorizeTargetResult> AuthorizeTargetResults
        {
            get { return _authorizeTargetResults.AsEnumerable(); }
        }

        /// <summary>
        /// Adds an <see cref="AuthorizeTargetResult"/> for an <see cref="ISecurityTarget"> Actor</see>
        /// </summary>
        /// <param name="resultToAdd">Result to add</param>
        public void AddAuthorizeTargetResult(AuthorizeTargetResult resultToAdd)
        {
            _authorizeTargetResults.Add(resultToAdd);
        }

        /// <summary>
        /// Gets the result of the Authorization for the <see cref="ISecurityTarget"/>
        /// </summary>
        public bool IsAuthorized
        {
            get { return AuthorizeTargetResults.All(r => r.IsAuthorized); }
        }
    }
}