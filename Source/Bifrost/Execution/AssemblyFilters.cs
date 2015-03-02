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

using Bifrost.Configuration.Assemblies;
namespace Bifrost.Execution
{
    /// <summary>
    /// Represents an implementation of <see cref="IAssemblyFilters"/>
    /// </summary>
    public class AssemblyFilters : IAssemblyFilters
    {
        AssembliesConfiguration _assembliesConfiguration;

        /// <summary>
        /// Initializes an instance of <see cref="AssemblyFilters"/>
        /// </summary>
        /// <param name="assembliesConfiguration"></param>
        public AssemblyFilters(AssembliesConfiguration assembliesConfiguration)
        {
            _assembliesConfiguration = assembliesConfiguration;
        }

#pragma warning disable 1591 // Xml Comments
        public bool ShouldInclude(string filename)
        {
            return _assembliesConfiguration.Specification.IsSatisfiedBy(filename);
        }
#pragma warning restore 1591 // Xml Comments

    }
}
