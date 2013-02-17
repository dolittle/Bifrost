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
using System.Collections.Generic;
using System.Security.Principal;
using System.Threading;

namespace Bifrost.Principal
{
    /// <summary>
    /// Represents the current principal.
    /// Thread.CurrentPrincipal substituted for explicit Principal for testing purposes.
    /// </summary>
    public class CurrentPrincipal : IDisposable
    {
        /// <summary>
        /// Gets the minimum time supported by the <see cref="SystemClock"/>
        /// </summary>
        public static readonly DateTime MinimumTime = new DateTime(1900, 1, 1);

        static readonly Stack<IPrincipal> _principals = new Stack<IPrincipal>();

        /// <summary>
        /// Retrieves the current principal
        /// </summary>
        /// <returns>Principal</returns>
        public static IPrincipal Get()
        {
            if (_principals.Count > 0)
                return _principals.Peek();
#if(NETFX_CORE)
            throw new NotImplementedException();
#else
            return Thread.CurrentPrincipal;
#endif
        }

        /// <summary>
        /// Allows the current principal to be set to an explicit value.  SOLELY FOR TESTING PURPOSES.
        /// Use within a "using" block within your spec so that the current principal is reset on exiting..
        /// </summary>
        /// <param name="principal">The explicit principal that you wish to set within the spec</param>
        /// <returns>A new instance of  CurrentPrincipal </returns>
        public static IDisposable SetPrincipalTo(IPrincipal principal)
        {
            _principals.Push(principal);
            return new CurrentPrincipal();
        }

        /// <summary>
        /// Will remove any explicitly set current time,
        /// </summary>
        public void Dispose()
        {
            if(_principals.Count > 0)
                _principals.Pop();
        }
    };
}