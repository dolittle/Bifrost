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
namespace Bifrost.Commands
{
	/// <summary>
	/// Invokes a command for a command handler type
	/// </summary>
	/// <remarks>
	/// Typically, the default invoker handles the generic
	/// <see cref="ICommandHandler">command handlers</see>
	/// </remarks>
	public interface ICommandHandlerInvoker
	{
		/// <summary>
		/// Try to handle a command
		/// 
		/// If it can handle it, it should handle it - and return true
		/// if it handled it and false if not
		/// </summary>
		/// <param name="command"><see cref="ICommand">Command to handle</see></param>
		/// <returns>True if it handled it, false if not</returns>
		bool TryHandle(ICommand command);
	}
}