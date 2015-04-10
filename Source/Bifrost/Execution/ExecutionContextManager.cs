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
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace Bifrost.Execution
{
    /// <summary>
    /// Represents a <see cref="IExecutionContextManager"/>
    /// </summary>
    public class ExecutionContextManager : IExecutionContextManager
    {
        /// <summary>
        /// Key identifying the current <see cref="IExectionContext"/> in a <see cref="ICallContext"/>
        /// </summary>
        public const string ExecutionContextKey = "ExecutionContext";

        IExecutionContextFactory _executionContextFactory;
        ICallContext _callContext;

        /// <summary>
        /// Initializes a new instance of <see cref="ExecutionContextManager"/>
        /// </summary>
        /// <param name="executionContextFactory"><see cref="IExecutionContextFactory"/> for creating <see cref="IExecutionContext">Exection Contexts</see></param>
        /// <param name="callContext"><see cref="ICallContext"/> to use for key/value store for holding current <see cref="IExecutionContext"/></param>
        public ExecutionContextManager(IExecutionContextFactory executionContextFactory, ICallContext callContext)
        {
            _executionContextFactory = executionContextFactory;
            _callContext = callContext;
        }


#pragma warning disable 1591 // Xml Comments
        public IExecutionContext Current
        {
            get 
            {
                IExecutionContext current = null;

                if (_callContext.HasData(ExecutionContextKey))
                    current = _callContext.GetData<IExecutionContext>(ExecutionContextKey);
                else
                {
                    current = _executionContextFactory.Create();
                    _callContext.SetData(ExecutionContextKey, current);
                }
                return current;
            }
        }
#pragma warning restore 1591 // Xml Comments
    }
}
