#region License
//
// Copyright (c) 2008-2014, Dolittle (http://www.dolittle.com)
//
// Licensed under the MIT License (http://opensource.org/licenses/MIT)
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at 
//
//   http://github.com/dolittle/Bifrost/blob/master/MIT-LICENSE.txt
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion
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

        /// <summary>
        /// Gets the <see cref="ISecurityActor"/> that this <see cref="AuthorizeActorResult"/> pertains to.
        /// </summary>
        public ISecurityActor Actor { get; private set; }

        /// <summary>
        /// Gets any <see cref="ISecurityRule"/> that were broken in the Authorization attempt.
        /// </summary>
        public IEnumerable<ISecurityRule> BrokenRules
        {
            get { return _brokenRules.AsEnumerable(); }
        }

        /// <summary>
        /// Add an instance of an <see cref="ISecurityRule"/> that was broken during Authorization
        /// </summary>
        /// <param name="rule">An instance of a broken <see cref="ISecurityRule"/></param>
        public void AddBrokenRule(ISecurityRule rule)
        {
            _brokenRules.Add(rule);
        }

        /// <summary>
        /// Add an instance of an <see cref="ISecurityRule"/> that was unable to be evaluted because it encountered an exception
        /// </summary>
        /// <param name="rule">The instance of the <see cref="ISecurityRule"/> that could not be evaluted.</param>
        /// <param name="exception">The exception that prevented the <see cref="ISecurityRule"/> from being evaluated.</param>
        public void AddErrorRule(ISecurityRule rule, Exception exception)
        {
            _rulesThatCausedError.Add(new RuleEvaluationError(rule,exception));
        }

        /// <summary>
        /// Gets any <see cref="RuleEvaluationError"/> that were encountered in the Authorization attempt.
        /// </summary>
        public IEnumerable<RuleEvaluationError> RulesThatEncounteredAnErrorWhenEvaluating
        {
            get { return _rulesThatCausedError.AsEnumerable(); }
        }

        /// <summary>
        /// Indicates whether the Authorization attempt was successful or not
        /// </summary>
        public virtual bool IsAuthorized
        {
            get { return !RulesThatEncounteredAnErrorWhenEvaluating.Any() && !BrokenRules.Any(); }
        }

        /// <summary>
        /// Builds a collection of strings that show Actor/Rule for each broken or erroring rule <see cref="AuthorizeActorResult"/>
        /// </summary>
        /// <returns>A collection of strings</returns>
        public virtual IEnumerable<string> BuildFailedAuthorizationMessages()
        {
            foreach (var brokenRule in BrokenRules)
                yield return Actor.Description + "/" + brokenRule.Description;

            foreach (var errorRule in RulesThatEncounteredAnErrorWhenEvaluating)
                yield return Actor.Description + "/" + errorRule.BuildErrorMessage();
        }
    }
}