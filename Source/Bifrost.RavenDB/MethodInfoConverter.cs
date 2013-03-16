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
using System.Linq;
using System.Reflection;
using Raven.Imports.Newtonsoft.Json;
using Raven.Imports.Newtonsoft.Json.Linq;

namespace Bifrost.RavenDB
{
    public class MethodInfoConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(MethodInfo).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            try
            {
                var jsonObject = JObject.Load(reader);
                var className = jsonObject.Property("ClassName").Value.ToString();
                var name = jsonObject.Property("Name").Value.ToString();
                var assemblyName = jsonObject.Property("AssemblyName").Value.ToString();
                var signature = jsonObject.Property("Signature").Value.ToString();

                var start = signature.IndexOf('(') + 1;
                var end = signature.IndexOf(')');
                var parametersString = signature.Substring(start, end - start);
                var parameterTypeStrings = parametersString.Split(',');

                if (string.IsNullOrEmpty(className) ||
                    string.IsNullOrEmpty(name) ||
                    string.IsNullOrEmpty(assemblyName) ||
                    parameterTypeStrings == null)
                    throw new ArgumentException("Json object is not representing a MethodInfo");

                var typeName = string.Format("{0}, {1}", className, assemblyName);
                var type = Type.GetType(typeName);
                var method = type.GetMethods().Where(m => m.Name.Equals(name) && DoesSignatureMatch(m, parameterTypeStrings)).SingleOrDefault();
                return method;
            }
            catch {
                return null;
            }
        }

        bool DoesSignatureMatch(MethodInfo m, string[] parameterTypeStrings)
        {
            var parameters = m.GetParameters().Select(p => p.ParameterType.FullName).ToArray();
            if (parameters.Length == parameterTypeStrings.Length)
                for (var parameterIndex = 0; parameterIndex < parameters.Length; parameterIndex++)
                    if (parameterTypeStrings[parameterIndex] != parameters[parameterIndex])
                        return false;

            return true;
        }

        public override bool CanWrite { get { return false; } }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
        }
    }
}
