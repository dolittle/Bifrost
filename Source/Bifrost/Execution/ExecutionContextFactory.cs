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
using System.Threading;
using Bifrost.Configuration;
using Bifrost.Security;
namespace Bifrost.Execution
{
    /// <summary>
    /// Represents a <see cref="IExecutionContextFactory"/>
    /// </summary>
    public class ExecutionContextFactory : IExecutionContextFactory
    {
        ICanResolvePrincipal _principalResolver;
        IExecutionContextDetailsPopulator _detailsPopulator;

        /// <summary>
        /// Initializes a new instance of <see cref="ExecutionContextFactory"/>
        /// </summary>
        /// <param name="principalResolver"><see cref="ICanResolvePrincipal"/> for resolving the identity</param>
        /// <param name="detailsPopulator">A <see cref="IExecutionContextDetailsPopulator"/> to use for populating any <see cref="IExecutionContext"/> being created</param>
        /// <param name="configure">A <see cref="IConfigure"/> instance holding all configuration</param>
        public ExecutionContextFactory(ICanResolvePrincipal principalResolver, IExecutionContextDetailsPopulator detailsPopulator, IConfigure configure)
        {
            _principalResolver = principalResolver;
            _detailsPopulator = detailsPopulator;
        }

#pragma warning disable 1591 // Xml Comments
        public IExecutionContext Create()
        {
            var principal = _principalResolver.Resolve();
            var culture = Thread.CurrentThread.CurrentCulture;
            var executionContext = new ExecutionContext(principal,culture,_detailsPopulator.Populate,null,null);
            return executionContext;
        }
#pragma warning restore 1591 // Xml Comments
    }
}
