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

        /// <summary>
        /// Gets or sets wether or not to include type names during serialization
        /// </summary>
        public bool IncludeTypeNames { get; set; }
	}
}