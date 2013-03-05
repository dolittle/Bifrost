#region License
//
// Copyright (c) 2008-2013, Dolittle (http://www.dolittle.com)
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

namespace Bifrost.Security
{
    /// <summary>
    /// Encapsulates a <see cref="ISecurityRule"/> that encountered an Exception when evaluating
    /// </summary>
    public class RuleEvaluationError
    {
        /// <summary>
        /// Instantiates an instance of <see cref="RuleEvaluationError"/>
        /// </summary>
        /// <param name="rule"><see cref="ISecurityRule"/> that encounted the error when evaluating.</param>
        /// <param name="error">The error that was encountered</param>
        public RuleEvaluationError(ISecurityRule rule, Exception error)
        {
            Error = error;
            Rule = rule;
        }

        /// <summary>
        /// Gets the Exception that was encountered when evaluation the rule
        /// </summary>
        public Exception Error { get; private set; }

        /// <summary>
        /// Gets the rule instance that encountered the exception when evaluation
        /// </summary>
        public ISecurityRule Rule { get; private set; }

        /// <summary>
        /// Returns a descriptive message describing the rule
        /// </summary>
        /// <returns>String descibing the rule</returns>
        public string BuildErrorMessage()
        {
            return Rule.Description + "/" + "Error";
        }
    }
}