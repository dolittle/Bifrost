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
using Bifrost.Serialization;

namespace Bifrost.Sagas
{
    /// <summary>
    /// Represents the options for serialization
    /// </summary>
    public class SagaSerializationOptions : SerializationOptions
    {
        private static readonly string[] SagaProperties = typeof (ISaga).GetProperties().Select(t => t.Name).ToArray();

        /// <summary>
        /// Initializes a new instance of <see cref="SagaSerializationOptions"/>
        /// </summary>
        public SagaSerializationOptions() : base(SerializationOptionsFlags.None)
        {
        }

        /// <summary>
        /// Returns false for properties of <see cref="ISaga"/>, otherwise returns true.
        /// </summary>
        public override bool ShouldSerializeProperty(Type type, string propertyName)
        {
            if (typeof (ISaga).GetTypeInfo().IsAssignableFrom(type))
            {
                return SagaProperties.All(sp => sp != propertyName);
            }

            return true;
        }
    }
}