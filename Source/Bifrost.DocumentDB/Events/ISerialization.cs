#region License
//
// Copyright (c) 2008-2014, Dolittle (http://www.dolittle.com)
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
using Microsoft.Azure.Documents;

namespace Bifrost.DocumentDB.Events
{
    /// <summary>
    /// Defines a serialization utility
    /// </summary>
    public interface ISerialization
    {
        /// <summary>
        /// Serialize an object to a stream
        /// </summary>
        /// <typeparam name="T">Type of the object to serialize</typeparam>
        /// <param name="source">Source object to serialize</param>
        /// <returns><see cref="Stream"/> representing the serialized object</returns>
        Stream ToStream<T>(T source);

        /// <summary>
        /// Deserialize from a document
        /// </summary>
        /// <param name="document"><see cref="Document"/> to deserialize from</param>
        /// <returns>The serialized type</returns>
        T FromDocument<T>(Document document);
    }
}
