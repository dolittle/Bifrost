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
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Bifrost.Execution;

namespace Bifrost.Entities.Files
{
    /// <summary>
    /// Represents an implementation of <see cref="IEntityContextConnection"/> for the simple file based <see cref="EntityContext{T}"/>
    /// </summary>
    public class EntityContextConnection : IEntityContextConnection
    {
        /// <summary>
        /// Initializes an instance of <see cref="EntityContextConnection"/>
        /// </summary>
        /// <param name="configuration"></param>
        public EntityContextConnection(EntityContextConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Gets the path for a specific type
        /// </summary>
        /// <typeparam name="T">Type of entity to get for</typeparam>
        /// <returns>Path for the entity</returns>
        public string GetPathFor<T>()
        {
            var type = typeof(T);
            var path = Path.Combine(Configuration.Path, string.Format("{0}.{1}", type.Namespace, type.Name));
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            return path;
        }

        /// <summary>
        /// Get all files for a specific entity
        /// </summary>
        /// <typeparam name="T">Type of entity to get for</typeparam>
        /// <returns></returns>
        public IEnumerable<string> GetAllFilesFor<T>()
        {
            var path = GetPathFor<T>();
            var files = Directory.GetFiles(path);
            return files;
        }

        /// <summary>
        /// Get the checksum from an entity - based on its JSON string representation
        /// </summary>
        /// <param name="json">JSON to get checksum from</param>
        /// <returns>A checksum</returns>
        public string GetEntityChecksum(string json)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(json);
            byte[] hash = md5.ComputeHash(inputBytes);

            var builder = new StringBuilder();
            foreach( var @byte in hash ) builder.Append(@byte);
            return builder.ToString();
        }

        /// <summary>
        /// Gets the configuration associated with the connection
        /// </summary>
        public EntityContextConfiguration Configuration { get; private set; }
        
#pragma warning disable 1591 // Xml Comments
        public void Initialize(IContainer container)
        {
        }
#pragma warning restore 1591 // Xml Comments
    }
}
