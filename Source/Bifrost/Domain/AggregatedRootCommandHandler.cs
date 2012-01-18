#region License
//
// Copyright (c) 2008-2012, DoLittle Studios AS and Komplett ASA
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
using Bifrost.Commands;

namespace Bifrost.Domain
{
	/// <summary>
	/// Generic CommandHandler for commands targetting AggregatedRoots
	/// </summary>
	/// <typeparam name="T">
	/// Type of <see cref="AggregatedRoot">AggregatedRoot</see> it handles
	/// </typeparam>
	/// <remarks>
	/// Purpose of this command handler is to do some of the repetitive tedious things one
	/// must do for handling commands for aggregated roots.
	/// 
	/// Typically all repository behavior and factory behavior
	/// </remarks>
	public class AggregatedRootCommandHandler<T> : ICommandHandlerForType<T>
		where T : AggregatedRoot
	{

		/// <summary>
		/// Constructs a <see cref="AggregatedRootCommandHandler{T}">AggregatedRootCommandHandler</see>
		/// </summary>
		public AggregatedRootCommandHandler()
		{
		}

		/// <summary>
		/// Handle the command
		/// </summary>
		/// <param name="commandForType">Command to handle</param>
		public void Handle(ICommandForType<T> commandForType)
		{
			
		}
	}
}