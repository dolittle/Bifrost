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

namespace Bifrost.Serialization
{
    /// <summary>
    /// Represents the options for serialization
    /// </summary>
	public class SerializationOptions
	{
        /// <summary>
        /// A func that gets called during serialization of properties to decide 
        /// </summary>
		public Func<Type, string, bool>	ShouldSerializeProperty = (t, p) => true;


        /// <summary>
        /// Gets or sets wether or not to use camel case for naming of properties
        /// </summary>
        public bool UseCamelCase { get; set; }
	}
}