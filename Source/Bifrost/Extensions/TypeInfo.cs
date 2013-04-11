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
#if(NETFX_CORE)
using System.Reflection;
#endif

namespace Bifrost.Extensions
{
    /// <summary>
    /// Represents an implementation of <see cref="ITypeInfo"/>
    /// </summary>
    /// <typeparam name="T">Type it holds info for</typeparam>
    public class TypeInfo<T> : ITypeInfo
    {
        /// <summary>
        /// Gets a singleton instance of the TypeInfo
        /// </summary>
        public static readonly TypeInfo<T> Instance = new TypeInfo<T>();

        TypeInfo()
        {
            var type = typeof(T); 
            HasDefaultConstructor = 
#if(NETFX_CORE)
                type.GetTypeInfo().IsValueType ||
                type.HasDefaultConstructor();
#else
                type.IsValueType ||
                type.GetConstructor(new Type[0]) != null ;
#endif
        }

#pragma warning disable 1591 // Xml Comments
        public bool HasDefaultConstructor { get; private set; }
#pragma warning restore 1591 // Xml Comments

    }
}
