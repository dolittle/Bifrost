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
using Bifrost.Events;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Options;

namespace Bifrost.MongoDB.Events
{
    public class EventSourceVersionSerializer : IBsonSerializer
    {
        public object Deserialize(BsonReader bsonReader, Type nominalType, Type actualType, IBsonSerializationOptions options)
        {
            var versionAsDouble = bsonReader.ReadDouble();
            var version = EventSourceVersion.FromCombined(versionAsDouble);
            return version;
        }

        public void Serialize(BsonWriter bsonWriter, Type nominalType, object value, IBsonSerializationOptions options)
        {
            var version = (EventSourceVersion)value;
            var versionAsDouble = version.Combine();
            bsonWriter.WriteDouble(versionAsDouble);
        }


        public object Deserialize(BsonReader bsonReader, Type nominalType, IBsonSerializationOptions options)
        {
            throw new NotImplementedException();
        }

        public bool GetDocumentId(object document, out object id, out Type idNominalType, out IIdGenerator idGenerator)
        {
            throw new NotImplementedException();
        }

        public void SetDocumentId(object document, object id)
        {
            throw new NotImplementedException();
        }

        public IBsonSerializationOptions GetDefaultSerializationOptions()
        {
            var options = new DocumentSerializationOptions();
            return options;
        }

    }
}
