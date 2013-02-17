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
	/// Defines a command handler for a specific type
	/// This is typically used for generic handling of commands
	/// </summary>
	/// <typeparam name="T">Type of object it can handle for</typeparam>
	/// <remarks>
	/// It works in correspondance with commands inheriting from 
	/// <see cref="ICommandForType{T}">ICommandForType</see>.
	/// 
	/// Also worth noting, you don't need to explicitly register these
	/// kinda handlers. The system will automatically find any type 
	/// implementing this type and automatically invoke it if a command
	/// comes into the system that corresponds to the type it supports.
	/// </remarks>
	public interface ICommandHandlerForType<T>
	{
		/// <summary>
		/// Handle a command
		/// </summary>
		/// <param name="commandForType">
		/// <see cref="ICommandForType{T}">ICommandForType</see> to handle
		/// </param>
		void Handle(ICommandForType<T> commandForType);
	}
}