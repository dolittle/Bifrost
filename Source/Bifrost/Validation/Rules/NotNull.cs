#region License
//
// Copyright (c) 2008-2015, Dolittle (http://www.dolittle.com)
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
using System.Reflection;
using Bifrost.Rules;

namespace Bifrost.Validation.Rules
{
    /// <summary>
    /// Represents the <see cref="ValueRule"/> for requiring the value to not be null
    /// </summary>
    public class NotNull : ValueRule
    {
        /// <summary>
        /// When a value is null, this is the reason given 
        /// </summary>
        public static BrokenRuleReason ValueIsNull = BrokenRuleReason.Create("712D26C6-A40F-4A3D-8C69-1475E761A1CF");

        /// <summary>
        /// Initializes a new instance of the <see cref="NotNull"/> rule
        /// </summary>
        /// <param name="property"><see cref="PropertyInfo">Property</see> the rule is for</param>
        public NotNull(PropertyInfo property) : base(property) { }

#pragma warning disable 1591 // Xml Comments
        public override void Evaluate(IRuleContext context, object instance)
        {
            if (instance == null) context.Fail(this, instance, ValueIsNull);
        }
#pragma warning restore 1591 // Xml Comments
    }
}
