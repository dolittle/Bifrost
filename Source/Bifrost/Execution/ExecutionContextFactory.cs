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
using System.Threading;
using Bifrost.Configuration;
using Bifrost.Security;
using Bifrost.Tenancy;

namespace Bifrost.Execution
{
    /// <summary>
    /// Represents a <see cref="IExecutionContextFactory"/>
    /// </summary>
    public class ExecutionContextFactory : IExecutionContextFactory
    {
        ICanResolvePrincipal _principalResolver;
        IExecutionContextDetailsPopulator _detailsPopulator;
        IConfigure _configure;
        ITenantManager _tenantManager;

        /// <summary>
        /// Initializes a new instance of <see cref="ExecutionContextFactory"/>
        /// </summary>
        /// <param name="principalResolver"><see cref="ICanResolvePrincipal"/> for resolving the identity</param>
        /// <param name="detailsPopulator">A <see cref="IExecutionContextDetailsPopulator"/> to use for populating any <see cref="IExecutionContext"/> being created</param>
        /// <param name="configure">A <see cref="IConfigure"/> instance holding all configuration</param>
        /// <param name="tenantManager">A <see cref="ITenantManager"/> to get <see cref="ITenant">tenants</see> from</param>
        public ExecutionContextFactory(ICanResolvePrincipal principalResolver, IExecutionContextDetailsPopulator detailsPopulator, IConfigure configure, ITenantManager tenantManager)
        {
            _principalResolver = principalResolver;
            _detailsPopulator = detailsPopulator;
            _configure = configure;
            _tenantManager = tenantManager;
        }

#pragma warning disable 1591 // Xml Comments
        public IExecutionContext Create()
        {
            var executionContext = new ExecutionContext(
                _principalResolver.Resolve(),
                Thread.CurrentThread.CurrentCulture,
                _detailsPopulator.Populate,
                _configure.SystemName);

            executionContext.Tenant = _tenantManager.Current;

            return executionContext;
        }
#pragma warning restore 1591 // Xml Comments
    }
}
