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
using Bifrost.Execution;
using Bifrost.JSON.Concepts;
using Bifrost.JSON.Events;
using Bifrost.JSON.Serialization;
using Bifrost.Serialization;
using Microsoft.Azure.Documents;
using Newtonsoft.Json;

namespace Bifrost.DocumentDB.Events
{
    /// <summary>
    /// Represents an implementation of <see cref="ISerialization"/>
    /// </summary>
    [Singleton]
    public class Serialization : ISerialization
    {
        JsonSerializer _serializer;

        /// <summary>
        /// Initializes a new instance of <see cref="Serialization"/>
        /// </summary>
        public Serialization(IContainer container)
        {
            _serializer = new JsonSerializer
            {
                ContractResolver = new SerializerContractResolver(container, new SerializationOptions())
            };
            _serializer.Converters.Add(new MethodInfoConverter());
            _serializer.Converters.Add(new ConceptConverter());
            _serializer.Converters.Add(new ConceptDictionaryConverter());
            _serializer.Converters.Add(new EventSourceVersionConverter());
        }

#pragma warning disable 1591
        public Stream ToStream<T>(T source)
        {
            var serialized = string.Empty;
            using (var stringWriter = new StringWriter())
            {
                _serializer.Serialize(stringWriter, source);
                serialized = stringWriter.ToString();
            }

            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(serialized);
            writer.Flush();
            stream.Position = 0;

            return stream;
        }

        public T FromDocument<T>(Document document)
        {
            var stream = new MemoryStream();
            document.SaveTo(stream);
            stream.Position = 0;
            var reader = new StreamReader(stream);
            var deserialized = (T)_serializer.Deserialize(reader, typeof(T));
            return deserialized;
        }

#pragma warning restore 1591
    }
}
