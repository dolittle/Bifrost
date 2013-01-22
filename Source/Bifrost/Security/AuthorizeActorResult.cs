using System;
using System.Collections.Generic;
using System.Linq;

namespace Bifrost.Security
{
    /// <summary>
    /// Represents the result of an authorization of a <see cref="ISecurityActor"/>
    /// </summary>
    public class AuthorizeActorResult
    {
        readonly List<ISecurityRule> _brokenRules = new List<ISecurityRule>();
        readonly List<RuleEvaluationError> _rulesThatCausedError = new List<RuleEvaluationError>();
        
        /// <summary>
        /// Instantiates an instance of <see cref="AuthorizeActorResult"/>
        /// </summary>
        /// <param name="actorThatResultIsFor">The <see cref="ISecurityActor"/> that this <see cref="AuthorizeActorResult"/> pertains to.</param>
        public AuthorizeActorResult(ISecurityActor actorThatResultIsFor)
        {
            Actor = actorThatResultIsFor;
        }

        public ISecurityActor Actor { get; private set; }

        public IEnumerable<ISecurityRule> BrokenRules
        {
            get { return _brokenRules.AsEnumerable(); }
        }

        public void AddBrokenRule(ISecurityRule rule)
        {
            _brokenRules.Add(rule);
        }

        public void AddErrorRule(ISecurityRule rule, Exception exception)
        {
            _rulesThatCausedError.Add(new RuleEvaluationError(rule,exception));
        }

        public IEnumerable<RuleEvaluationError> RulesThatEncounteredAnErrorWhenEvaluating
        {
            get { return _rulesThatCausedError.AsEnumerable(); }
        }

        public bool IsAuthorized
        {
            get { return !RulesThatEncounteredAnErrorWhenEvaluating.Any() && !BrokenRules.Any(); }
        }
    }
}