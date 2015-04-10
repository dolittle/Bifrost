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
using System.Globalization;
using System.Security.Principal;
using Bifrost.Tenancy;

namespace Bifrost.Execution
{
    /// <summary>
    /// Represents a <see cref="IExecutionContext"/>
    /// </summary>
    [IgnoreDefaultConvention]
    public class ExecutionContext : IExecutionContext
    {
        /// <summary>
        /// Initializes an instance of <see cref="ExecutionContext"/>
        /// </summary>
        /// <param name="principal"><see cref="IPrincipal"/> to populate with</param>
        /// <param name="cultureInfo"><see cref="CultureInfo"/> for the <see cref="ExecutionContext"/></param>
        /// <param name="detailsPopulator">Callback that gets called for populating the details of the <see cref="ExecutionContext"/></param>
        /// <param name="system">Name of the system that is running</param>
        public ExecutionContext(IPrincipal principal, CultureInfo cultureInfo, ExecutionContextPopulator detailsPopulator, string system)
        {
            Principal = principal;
            Culture = cultureInfo;
            System = system;
            Details = new WriteOnceExpandoObject(d => detailsPopulator(this,d));
        }

#pragma warning disable 1591 // Xml Comments
        public IPrincipal Principal { get; private set; }
        public CultureInfo Culture { get; private set; }
        public string System { get; private set; }
        public ITenant Tenant { get; set; }
        public dynamic Details { get; private set; }
#pragma warning restore 1591 // Xml Comments
    }
}
