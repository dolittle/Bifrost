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

namespace Bifrost.Commands
{
    /// <summary>
    /// The exception that is thrown when a command is not known by its name in the system
    /// </summary>
	public class UnknownCommandException : ArgumentException
	{
        /// <summary>
        /// Initializes a new instance of <see cref="UnknownCommandException"/>
        /// </summary>
        /// <param name="name"></param>
		public UnknownCommandException (string name) : base("There is no command called : "+name)
		{
		}
	}
}

