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

using System.IO;
using Bifrost.Entities.Files;
namespace Bifrost.Configuration
{
    /// <summary>
    /// Extensions for configuration
    /// </summary>
    public static partial class ConfigurationExtensions
    {

        /// <summary>
        /// Configures <see cref="IHaveStorage">storage</see> to use a simple files system
        /// </summary>
        /// <param name="storage"><see cref="IHaveStorage">Storage</see> to configure</param>
        /// <param name="path">Path to store files</param>
        /// <returns>Chained <see cref="IConfigure"/> for fluent configuration</returns>
        public static IConfigure UsingFiles(this IHaveStorage storage, string path)
        {
            if (!Path.IsPathRooted(path))
                path = Path.Combine(Directory.GetCurrentDirectory(), path);

            var configuration = new EntityContextConfiguration
            {
                Path = path
            };

            configuration.Connection = new EntityContextConnection(configuration);

            storage.EntityContextConfiguration = configuration;

            return Configure.Instance;
        }
    }
}
