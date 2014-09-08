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
using System.Linq.Expressions;
namespace Bifrost.Read.Validation
{
    /// <summary>
    /// Represents the basis for a validation descriptor for describing validation for queries
    /// </summary>
    /// <typeparam name="TQuery">Type of <see cref="IQuery"/> descriptor is for</typeparam>
    public class QueryValidationDescriptorFor<TQuery> where TQuery : IQuery
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public QueryArgumentValidationBuilder<TQuery,TArgument> ForArgument<TArgument>(Expression<Func<TQuery, TArgument>> expression)
        {
            var builder = new QueryArgumentValidationBuilder<TQuery, TArgument>(expression);
            return builder;
        }
    }
}
