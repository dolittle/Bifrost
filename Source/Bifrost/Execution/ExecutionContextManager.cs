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
using System.Threading;

namespace Bifrost.Execution
{
    /// <summary>
    /// Represents a <see cref="IExecutionContextManager"/>
    /// </summary>
    public class ExecutionContextManager : IExecutionContextManager
    {
        [ThreadStatic] static IExecutionContext _current;

        bool HasCurrent { get { return _current != null; } }


#pragma warning disable 1591 // Xml Comments
        public void Reset()
        {
            _current = null;
        }

        public IExecutionContext Current
        {
            get 
            {
                if (!HasCurrent)
                {
                    _current = new ExecutionContext
                    {
#if(!SILVERLIGHT && !NETFX_CORE)
                        // Todo : Figure out the best way to get the current user - probably get it from the server side of things.
                        Identity = Thread.CurrentPrincipal.Identity,
#endif
                        System = "[Unknown]"
                    };
                }
                return _current;
            }
        }
#pragma warning restore 1591 // Xml Comments

    }
}
