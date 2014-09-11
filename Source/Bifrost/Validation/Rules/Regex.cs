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
using Bifrost.Rules;

namespace Bifrost.Validation.Rules
{
    /// <summary>
    /// Represents the <see cref="ValueRule"/> for specific regular expression - any value must conform with a regular expression
    /// </summary>
    public class Regex : ValueRule
    {
        /// <summary>
        /// When a string does not conform to the specified expression, this is the reason given
        /// </summary>
        public static BrokenRuleReason NotConformingToExpression = BrokenRuleReason.Create("BE58A125-40DB-47EA-B260-37F7AF4455C5");

        System.Text.RegularExpressions.Regex _actualRegex;

        /// <summary>
        /// Initializes an instance of <see cref="Regex"/>
        /// </summary>
        /// <param name="expression"></param>
        public Regex(string expression)
        {
            Expression = expression;
            _actualRegex = new System.Text.RegularExpressions.Regex(expression);
        }

        /// <summary>
        /// Get the expression that values must conform to
        /// </summary>
        public string Expression { get; private set; }

#pragma warning disable 1591 // Xml Comments
        public override void Evaluate(IRuleContext context, object instance)
        {
            if (FailIfValueTypeMismatch<string>(context, instance))
            {
                if (!_actualRegex.IsMatch((string)instance)) context.Fail(this, instance, NotConformingToExpression);
            }
        }
#pragma warning restore 1591 // Xml Comments
    }
}
