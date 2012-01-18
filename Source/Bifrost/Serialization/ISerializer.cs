#region License
//
// Copyright (c) 2008-2012, DoLittle Studios AS and Komplett ASA
//
// Licensed under the Microsoft Permissive License (Ms-PL), Version 1.1 (the "License")
// With one exception :
//   Commercial libraries that is based partly or fully on Bifrost and is sold commercially, 
//   must obtain a commercial license.
//
// You may not use file except in compliance with the License.
// You may obtain a copy of the license at 
//
//   http://bifrost.codeplex.com/license
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

namespace Bifrost.Serialization
{
    /// <summary>
    /// Defines a serializer
    /// </summary>
	public interface ISerializer
	{
		/// <summary>
		/// Deserialize Json to a specific type from a <see cref="string"/>
		/// </summary>
		/// <typeparam name="T">Type to deserialize to</typeparam>
		/// <param name="json"><see cref="string"/> containing the Json</param>
		/// <param name="options">Options for the serializer</param>
		/// <returns>An deserialized</returns>
		T FromJson<T>(string json, SerializationOptions options = null);

		/// <summary>
		/// Deserialize Json to a specific type from a <see cref="string"/>
		/// </summary>
		/// <param name="type">Type to deserialize to</param>
		/// <param name="json"><see cref="string"/> containing the Json</param>
		/// <param name="options">Options for the serializer</param>
		/// <returns>A deserialized instance</returns>
		object FromJson(Type type, string json, SerializationOptions options = null);

		/// <summary>
		/// Deserialize Json into a specific instance
		/// </summary>
		/// <param name="instance">Instance to deserialize into</param>
		/// <param name="json"><see cref="string"/> containing the Json</param>
		/// <param name="options">Options for the serializer</param>
		void FromJson(object instance, string json, SerializationOptions options = null);

		/// <summary>
		/// Serialize an object to Json as a string
		/// </summary>
		/// <param name="instance">Instance to serialize</param>
		/// <param name="options">Options for the serializer</param>
		/// <returns><see cref="string"/> containing the serialized instance</returns>
		string ToJson(object instance, SerializationOptions options = null);

		/// <summary>
		/// Deserialize Json into a key/value dictionary
		/// </summary>
		/// <param name="json">Json to deserialize</param>
		/// <returns>A dictionary holding all properties and values in the Json</returns>
		IDictionary<string, object> GetKeyValuesFromJson(string json);
	}
}
