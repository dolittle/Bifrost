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
using System;
using System.Reflection;

namespace Bifrost.Execution
{
    /// <summary>
    /// Exception that is thrown when a target is not alive in a weak reference
    /// </summary>
    public class CannotInvokeMethodBecauseTargetIsNotAlive : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of <see cref="CannotInvokeMethodBecauseTargetIsNotAlive"/>
        /// </summary>
        /// <param name="method"></param>
        public CannotInvokeMethodBecauseTargetIsNotAlive(MethodInfo method) : base(string.Format("Method '{0}' can't be invoked, since target has been collected by the garbage collector", method)) { }
    }
}
