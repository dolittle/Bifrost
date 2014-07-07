#region License
//
// Copyright (c) 2008-2014, Dolittle (http://www.dolittle.com)
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
using System.Collections.Generic;
using System.Linq;
using Bifrost.Execution;
using Bifrost.Extensions;
using Bifrost.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Bifrost.JSON.Serialization
{
    /// <summary>
    /// Represents a <see cref="IContractResolver"/> based on the <see cref="DefaultContractResolver"/> for resolving contracts for serialization
    /// </summary>
    public class SerializerContractResolver : DefaultContractResolver
    {

        readonly IContainer _container;
		readonly SerializationOptions _options;

        /// <summary>
        /// Initializes a new instance of <see cref="SerializerContractResolver"/>
        /// </summary>
        /// <param name="container">A <see cref="IContainer"/> to use for creating instances of types</param>
        /// <param name="options"><see cref="SerializationOptions"/> to use during resolving</param>
		public SerializerContractResolver(IContainer container, SerializationOptions options) : base(true)
		{
			_container = container;
			_options = options;
		}


#pragma warning disable 1591 // Xml Comments
        // Todo : figure out why Silverlight doesn't have this - vital for serialization
#if(!SILVERLIGHT)
		protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
		{
			var properties = base.CreateProperties(type, memberSerialization);
			if( _options != null )
				return properties.Where(p => _options.ShouldSerializeProperty(type, p.PropertyName)).ToList();

			return properties;
		}

#endif

        public override JsonContract ResolveContract(Type type)
		{
			var contract = base.ResolveContract(type);
		
			if (contract is JsonObjectContract && 
                !type.IsValueType &&
                !type.HasDefaultConstructor())
			{
				var defaultCreator = contract.DefaultCreator;
				contract.DefaultCreator = () =>
				                          	{
				                          		try
				                          		{
                                                    // Todo: Structs without default constructor will fail with this and that will then try using the defaultCreator in the catch
				                          			return _container.Get(type);
				                          		}
				                          		catch
				                          		{
				                          			return defaultCreator();
				                          		}
				                          	};
			}

			return contract;
        }

        protected override string ResolvePropertyName(string propertyName)
        {
            var result = base.ResolvePropertyName(propertyName);
            if (_options != null && _options.UseCamelCase)
                result = result.ToCamelCase();
            return result;
        }
#pragma warning restore 1591 // Xml Comments
    }
}