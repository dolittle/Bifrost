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

namespace Bifrost.Execution
{
    /// <summary>
    /// Represents a <see cref="IExecutionContextManager"/>
    /// </summary>
    public class ExecutionContextManager : IExecutionContextManager
    {
        [ThreadStatic] static IExecutionContext _current;
        bool HasCurrent { get { return _current != null; } }


        IExecutionContextFactory _executionContextFactory;

        /// <summary>
        /// Initializes a new instance of <see cref="ExecutionContextManager"/>
        /// </summary>
        /// <param name="executionContextFactory"><see cref="IExecutionContextFactory"/> for creating <see cref="IExecutionContext">Exection Contexts</see></param>
        public ExecutionContextManager(IExecutionContextFactory executionContextFactory)
        {
            _executionContextFactory = executionContextFactory;
        }


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
                    _current = _executionContextFactory.Create();
                return _current;
            }
        }
#pragma warning restore 1591 // Xml Comments

    }
}
