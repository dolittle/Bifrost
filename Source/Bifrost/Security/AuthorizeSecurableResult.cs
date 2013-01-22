using System.Collections.Generic;
using System.Linq;

namespace Bifrost.Security
{
    /// <summary>
    /// Represents the result of an authorization of a <see cref="ISecurable"/>
    /// </summary>
    public class AuthorizeSecurableResult
    {
        readonly List<AuthorizeActorResult> _authorizeActorResults = new List<AuthorizeActorResult>();

        public AuthorizeSecurableResult(ISecurable securable)
        {
            Securable = securable;
        }

        /// <summary>
        /// Gets the <see cref="ISecurable"/> that this <see cref="AuthorizeSecurableResult"/> pertains to.
        /// </summary>
        public ISecurable Securable { get; private set; }

        /// <summary>
        /// Gets the <see cref="AuthorizeActorResult"/> for all <see cref="ISecurityActor"> Actors </see> on the <see cref="ISecurable"/>
        /// </summary>
        public IEnumerable<AuthorizeActorResult> AuthorizeActorResults
        {
            get { return _authorizeActorResults.AsEnumerable(); }
        }

        /// <summary>
        /// Adds an <see cref="AuthorizeActorResult"/> for an <see cref="ISecurityActor"> Actor</see>
        /// </summary>
        /// <param name="authorizeActorResult">Result to add</param>
        public void AddAuthorizeActorResult(AuthorizeActorResult authorizeActorResult)
        {
            _authorizeActorResults.Add(authorizeActorResult);
        }

        /// <summary>
        /// Gets the result of the Authorization for the <see cref="ISecurable"/>
        /// </summary>
        public bool IsAuthorized
        {
            get { return AuthorizeActorResults.All(r => r.IsAuthorized); }
        }
    }
}