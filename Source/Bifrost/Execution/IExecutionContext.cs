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
using System.Globalization;
using System.Security.Principal;
using Bifrost.Tenancy;

namespace Bifrost.Execution
{
    /// <summary>
    /// Defines the execution context in which things are within
    /// For instance, any commands coming into the system will be in the context of an execution context
    /// </summary>
    public interface IExecutionContext
    {
        /// <summary>
        /// Gets the <see cref="IPrincipal"/> for the execution context
        /// </summary>
        IPrincipal Principal { get; }

        /// <summary>
        /// Gets the <see cref="CultureInfo"/> for the execution context
        /// </summary>
        CultureInfo Culture { get; }

        /// <summary>
        /// Gets the string identifying the currently executing system
        /// </summary>
        string System { get; }

        /// <summary>
        /// Gets the tenant for the current execution context
        /// </summary>
        ITenant Tenant { get; }

        /// <summary>
        /// Gets the details for the execution context
        /// </summary>
        /// <remarks>
        /// This object is a write once object, meaning that you can't write to it at will.
        /// It can be populated by implementing a <see cref="ICanPopulateExecutionContextDetails"/>
        /// </remarks>
        WriteOnceExpandoObject Details { get; }
    }
}
