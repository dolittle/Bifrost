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
using System.Collections.Generic;
using System.Linq;

namespace Bifrost.Extensions
{
    /// <summary>
    /// Provides a set of extension methods for different collection and enumerable types
    /// </summary>
    public static class CollectionsExtensions
    {
        /// <summary>
        /// Enumerate an enumerable and call the given Action for each item
        /// </summary>
        /// <typeparam name="T">Item type</typeparam>
        /// <param name="enumerable"><see cref="IEnumerable{T}"/> to enumerate</param>
        /// <param name="action"><see cref="Action{T}"/> to call for each item</param>
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var item in enumerable)
                action(item);
        }

        /// <summary>
        /// Combines multiple lookups into a single lookup
        /// </summary>
        /// <typeparam name="TKey">The type of the keys</typeparam>
        /// <typeparam name="TElement">The type of the elements</typeparam>
        /// <param name="lookups">A collection of lookups to combine</param>
        /// <returns>A single lookup which takes a key into all values with this key in all incoming lookups</returns>
        public static ILookup<TKey, TElement> Combine<TKey, TElement>(this IEnumerable<ILookup<TKey, TElement>> lookups)
        {
            return lookups
                .SelectMany(l => l)
                .SelectMany(l => l.Select(value => new {l.Key, Value = value}))
                .ToLookup(x => x.Key, x => x.Value);
        }
    }
}
