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
using System.Linq;
using System.Reflection;
using System.Text;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Options;

namespace Bifrost.MongoDB.Events
{
    public class MethodInfoSerializer : IBsonSerializer
    {
        public object Deserialize(BsonReader bsonReader, Type nominalType, Type actualType, IBsonSerializationOptions options)
        {
            bsonReader.ReadStartDocument();

            bsonReader.ReadName();
            var typeName = bsonReader.ReadString();
            bsonReader.ReadName();
            var methodSignature = bsonReader.ReadString();

            bsonReader.ReadEndDocument();

            var type = Type.GetType(typeName);
            if (type != null)
            {
                var method = type.GetMethods().Where(m => GetMethodSignature(m) == methodSignature).SingleOrDefault();
                return method;
            }

            return null;
        }

        public void Serialize(BsonWriter bsonWriter, Type nominalType, object value, IBsonSerializationOptions options)
        {
            var method = (MethodInfo)value;

            bsonWriter.WriteStartDocument();
            bsonWriter.WriteName("Type");
            bsonWriter.WriteString(method.DeclaringType.AssemblyQualifiedName);
            bsonWriter.WriteName("Method");
            bsonWriter.WriteString(GetMethodSignature(method));

            bsonWriter.WriteEndDocument();

        }

        string GetMethodSignature(MethodInfo method)
        {
            var builder = new StringBuilder();
            builder.Append(method.Name);
            builder.Append("(");

            foreach (var parameter in method.GetParameters())
                builder.AppendFormat("{0} {1}", parameter.ParameterType.Name, parameter.Name);

            builder.Append(")");
            return builder.ToString();
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
