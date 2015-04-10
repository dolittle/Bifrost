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

namespace Bifrost.Execution
{
    /// <summary>
    /// Exception that is thrown when a type is not possible to be resolved by its name
    /// </summary>
    public class UnableToResolveTypeByName : ArgumentException
    {
        /// <summary>
        /// Initializes an instance of <see cref="UnableToResolveTypeByName"/>
        /// </summary>
        /// <param name="typeName"></param>
        public UnableToResolveTypeByName(string typeName) : base(string.Format("Unable to resolve '{0}'. More than one type found with the current name'", typeName))
        {

        }
    }
}
