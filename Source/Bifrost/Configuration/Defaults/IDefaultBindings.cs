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

using Bifrost.Execution;

namespace Bifrost.Configuration.Defaults
{
	/// <summary>
	/// Defines a system that sets up default bindings
	/// </summary>
    public interface IDefaultBindings
    {
		/// <summary>
		/// Initialize the bindings with the given container
		/// </summary>
		/// <param name="container">The <see cref="IContainer"/> to define the bindings with</param>
        void Initialize(IContainer container);
    }
}