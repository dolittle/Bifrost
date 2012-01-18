#region License
//
// Copyright (c) 2008-2012, DoLittle Studios and Komplett ASA
//
// Licensed under the Microsoft Permissive License (Ms-PL), Version 1.1 (the "License")
// With one exception :
//   Commercial libraries that is based partly or fully on Bifrost and is sold commercially, 
//   must obtain a commercial license.
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at 
//
//   http://bifrost.codeplex.com/license
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