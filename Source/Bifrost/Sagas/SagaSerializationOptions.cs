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
using Bifrost.Serialization;

#if(NETFX_CORE)
using System.Reflection;
#endif

namespace Bifrost.Sagas
{
    /// <summary>
    /// Represents the options for serialization
    /// </summary>
    public class SagaSerializationOptions : SerializationOptions
    {
#if (NETFX_CORE)
        static readonly string[] SagaProperties = typeof(ISaga).GetTypeInfo().DeclaredProperties.Select(t => t.Name).ToArray();
#else
        private static readonly string[] SagaProperties = typeof (ISaga).GetProperties().Select(t => t.Name).ToArray();
#endif

        /// <summary>
        /// Initializes a new instance of <see cref="SagaSerializationOptions"/>
        /// </summary>
        public SagaSerializationOptions() : base(SerializationOptionsFlags.None)
        {
        }

        /// <summary>
        /// Will always return true
        /// </summary>
        public override bool ShouldSerializeProperty(Type type, string propertyName)
        {
            if (typeof (ISaga)
#if (NETFX_CORE)
                .GetTypeInfo().IsAssignableFrom(type.GetTypeInfo())
#else
                .IsAssignableFrom(type)
#endif
                )
            {
                return SagaProperties.All(sp => sp != propertyName);
            }

            return true;
        }
    }
}