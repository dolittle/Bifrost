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
using System.Security.Principal;
using Bifrost.Tenancy;

namespace Bifrost.Execution
{
    /// <summary>
    /// Represents a <see cref="IExecutionContext"/>
    /// </summary>
    public class ExecutionContext : IExecutionContext
    {
        /// <summary>
        /// Initializes an instance of <see cref="ExecutionContext"/>
        /// </summary>
        /// <param name="principal"><see cref="IPrincipal"/> to populate with</param>
        /// <param name="detailsPopulator"></param>
        /// <param name="system">Name of the system that is running</param>
        /// <param name="tenant">The current tenant information <see cref="Tenant"/></param>
        public ExecutionContext(IPrincipal principal, Action<dynamic> detailsPopulator, string system, Tenant tenant)
        {
            Principal = principal;
            System = system;
            Tenant = tenant;
            Details = new WriteOnceExpandoObject(detailsPopulator);
        }

#pragma warning disable 1591 // Xml Comments
        public IPrincipal Principal { get; private set; }
        public string System { get; private set; }
        public Tenant Tenant { get; private set; }
        public WriteOnceExpandoObject Details { get; private set; }
#pragma warning restore 1591 // Xml Comments
    }
}
