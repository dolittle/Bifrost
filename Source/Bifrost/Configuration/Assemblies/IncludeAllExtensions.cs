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
using System.Reflection;
using Bifrost.Extensions;
using Bifrost.Specifications;

namespace Bifrost.Configuration.Assemblies
{
    /// <summary>
    /// Extensions for <see cref="IncludeAll"/>
    /// </summary>
    public static class IncludeAllExtensions
    {
        /// <summary>
        /// Include all except for assemblies that has a name starting with the given name
        /// </summary>
        /// <param name="includeAll"><see cref="IncludeAll">Configuration object</see></param>
        /// <param name="assemblyNames">Names of assemblies to exclude</param>
        /// <returns>Chain of <see cref="IncludeAll">configuration object</see></returns>
        public static IncludeAll ExceptAssembliesStartingWith(this IncludeAll includeAll, params string[] assemblyNames)
        {
            var specification = includeAll.Specification;
            assemblyNames.ForEach(assemblyName => specification = specification.And(new ExceptAssembliesStartingWith(assemblyName)));
            includeAll.Specification = specification;
            return includeAll;
        }
    }
}
