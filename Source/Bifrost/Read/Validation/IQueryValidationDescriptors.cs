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
namespace Bifrost.Read.Validation
{
    /// <summary>
    /// Defines a system for accessing validation descriptors for queries
    /// </summary>
    public interface IQueryValidationDescriptors
    {
        /// <summary>
        /// Checks if there is a <see cref="QueryValidationDescriptor{TQ}"/> for a specific <see cref="IQuery"/> by its type
        /// </summary>
        /// <typeparam name="TQuery">Type of <see cref="IQuery"/> to check for</typeparam>
        /// <returns>True if there is a <see cref="QueryValidationDescriptor{TQ}"/> for the query and false if not</returns>
        bool HasDescriptorFor<TQuery>() where TQuery : IQuery;

        /// <summary>
        /// Get a <see cref="QueryValidationDescriptor{TQ}"/> for a specific <see cref="IQuery"/> by its type
        /// </summary>
        /// <typeparam name="TQuery">Type of <see cref="IQuery"/> to get for</typeparam>
        /// <returns><see cref="QueryValidationDescriptor{TQ}"/> describing the validation for the <see cref="IQuery"/></returns>
        QueryValidationDescriptorFor<TQuery> GetDescriptorFor<TQuery>() where TQuery : IQuery;
    }
}
