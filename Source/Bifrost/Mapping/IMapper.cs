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

namespace Bifrost.Mapping
{
    /// <summary>
    /// Defines a mapper that is capable of mapping objects
    /// </summary>
    public interface IMapper
    {
        /// <summary>
        /// Map an existing object and create a new while doing so
        /// </summary>
        /// <typeparam name="TTarget">Target type to create</typeparam>
        /// <typeparam name="TSource">Type of the source we're mapping from</typeparam>
        /// <param name="source">Source object</param>
        /// <returns>A new mapped instance</returns>
        TTarget Map<TTarget, TSource>(TSource source);

        /// <summary>
        /// Map to an existing instance of an object
        /// </summary>
        /// <typeparam name="TTarget">Type of the target we're mapping to</typeparam>
        /// <typeparam name="TSource">Type of the source we're mapping from</typeparam>
        /// <param name="source">Source object</param>
        /// <param name="target">Target object</param>
        void MapToInstance<TTarget, TSource>(TSource source, TTarget target);

    }
}
