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
using System.Linq.Expressions;

namespace Bifrost.Read
{
    /// <summary>
    /// Defines a way of retrieving single <see cref="IReadModel"/>
    /// </summary>
    /// <typeparam name="T">Type of <see cref="IReadModel"/> it retrieves</typeparam>
    public interface IReadModelOf<T> where T:IReadModel
    {
        /// <summary>
        /// Filter by properties
        /// </summary>
        /// <param name="propertyExpressions">Property filter expressions to use</param>
        /// <returns>An instance or default / null of the <see cref="IReadModel"/>, throws an exception if there is not a unique match</returns>
        T InstanceMatching(params Expression<Func<T, bool>>[] propertyExpressions);
    }
}
