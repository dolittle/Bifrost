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
using Castle.DynamicProxy;

namespace Bifrost.Reflection
{
    /// <summary>
    /// Defines a system that can handle invocations from an interface and delegate it to a concrete
    /// instance
    public interface ICanHandleInvocations
    {
        /// <summary>
        /// Gets asked for wether or not it can handle a specific <see cref="IInvocation"/>
        /// </summary>
        /// <param name="invocation"><see cref="IInvocation"/> to ask for</param>
        /// <returns>True if it can handle it, false if not</returns>
        bool CanHandle(IInvocation invocation);

        /// <summary>
        /// Handel a specific <see cref="IInvocation"/>
        /// </summary>
        /// <param name="invocation"><see cref="IInvocation"/> to handle</param>
        void Handle(IInvocation invocation);

    }
}
