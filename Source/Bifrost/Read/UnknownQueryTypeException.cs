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

namespace Bifrost.Read
{
    /// <summary>
    /// The exception that is thrown when a well known query does not have the query property on it
    /// </summary>
    public class UnknownQueryTypeException : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of <see cref="UnknownQueryTypeException"/>
        /// </summary>
        /// <param name="query"><see cref="IQuery"/> that does not have the property on it</param>
        /// <param name="type"><see cref="Type"/> of the expected query returned from the Query property</param>
        public UnknownQueryTypeException(IQuery query, Type type)
            : base(string.Format("Unable to find a query provider of type '{0}' for the query '{1}'. Hint: Are you sure the query return type has a known query provider for it?", type.FullName, query.GetType().FullName))
        {
        }
    }
}
