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
using System.Reflection;

namespace Bifrost.Mapping
{
    /// <summary>
    /// Defines a mapping target - more specifically you should look at <see cref="IMappingTargetFor"/>
    /// </summary>
    public interface IMappingTarget
    {
        /// <summary>
        /// Gets the type of target object
        /// </summary>
        Type TargetType { get; }

        /// <summary>
        /// Set value for a member with a given value
        /// </summary>
        /// <param name="target">Target object to set value for</param>
        /// <param name="member"><see cref="MemberInfo"/> to set for</param>
        /// <param name="value">Actual value to set</param>
        void SetValue(object target, MemberInfo member, object value);
    }
}