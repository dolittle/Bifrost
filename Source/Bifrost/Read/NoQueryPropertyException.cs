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

namespace Bifrost.Read
{
    /// <summary>
    /// The exception that is thrown when a well known query does not have the query property on it
    /// </summary>
    public class NoQueryPropertyException : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of <see cref="NoQueryPropertyException"/>
        /// </summary>
        /// <param name="query"><see cref="IQuery"/> that does not have the property on it</param>
        public NoQueryPropertyException(IQuery query)
            : base(string.Format("No query property for {0}. Hint: It should be a public instance property with a get on it.", query.GetType().FullName))
        {
        }
    }
}
