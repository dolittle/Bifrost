#region License
//
// Copyright (c) 2008-2015, Dolittle (http://www.dolittle.com)
//
// Licensed under the MIT License (http://opensource.org/licenses/MIT)
// With one exception :
//   Commercial libraries that is based partly or fully on Bifrost and is sold commercially,
//   must obtain a commercial license.
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
using Bifrost.Serialization;
using Microsoft.Azure.Documents;

namespace Bifrost.DocumentDB.Events
{
    /// <summary>
    /// Extends <see cref="ISerializer"/> with helper functionality
    /// </summary>
    public static class SerializationExtensions
    {
        internal static ISerializationOptions CamelCaseOptions = SerializationOptions.CamelCase;

        /// <summary>
        /// Deserialize from a document
        /// </summary>
        /// <param name="serializer"><see cref="ISerializer"/> that is extended</param>
        /// <param name="document"><see cref="Document"/> to deserialize from</param>
        /// <returns>The serialized type</returns>
        public static T FromDocument<T>(this ISerializer serializer, Document document)
        {
            var stream = new MemoryStream();
            document.SaveTo(stream);
            stream.Position = 0;
            var reader = new StreamReader(stream);
            var jsonAsString = reader.ReadToEnd();
            var deserialized = (T)serializer.FromJson(typeof(T), jsonAsString, CamelCaseOptions);
            return deserialized;
        }
    }
}
