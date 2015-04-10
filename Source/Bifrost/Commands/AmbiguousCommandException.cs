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
using System.Text;

namespace Bifrost.Commands
{
    /// <summary>
    /// The exception that is thrown if two commands have the same type name
    /// </summary>
    public class AmbiguousCommandException : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of <see cref="AmbiguousCommandException"/>
        /// </summary>
        /// <param name="first">The existing command - first discovered</param>
        /// <param name="second">The second command discovered that has the same name</param>
        public AmbiguousCommandException(Type first, Type second) 
            : base
                (
                    string.Format
                        ("Command '{0}' has the same name as '{1}', names must be unique across an application",
                            second.AssemblyQualifiedName, first.AssemblyQualifiedName
                        )
                ) { }
    }
}
