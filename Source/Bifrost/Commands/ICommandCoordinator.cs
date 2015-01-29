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
using Bifrost.Sagas;

namespace Bifrost.Commands
{
	/// <summary>
	/// Defines a coordinator for coordinating commands coming into the system
	/// </summary>
	public partial interface ICommandCoordinator
	{
		/// <summary>
		/// Handle a command in the context of a saga
		/// </summary>
		/// <param name="saga"><see cref="ISaga"/> to handle in context of</param>
		/// <param name="command"><see cref="ICommand">command</see> to handle</param>
		/// <returns>
		/// Result from the handle.
		/// Within the result one can check if the handling was success or not
		/// </returns>
		CommandResult Handle(ISaga saga, ICommand command);

		/// <summary>
		/// Handle a command
		/// </summary>
		/// <param name="command"><see cref="ICommand">command</see> to handle</param>
		/// <returns>
		/// Result from the handle.
		/// Within the result one can check if the handling was success or not
		/// </returns>
		CommandResult Handle(ICommand command);
	}
}
