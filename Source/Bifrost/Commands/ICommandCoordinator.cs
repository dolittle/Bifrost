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

using System;
using System.Linq.Expressions;
using Bifrost.Domain;
using Bifrost.Sagas;

namespace Bifrost.Commands
{
	/// <summary>
	/// Defines a coordinator for coordinating commands coming into the system
	/// </summary>
	public interface ICommandCoordinator
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

		/// <summary>
		/// Handle a command by using a method as definition on the aggregated root 
		/// and using a dynamic command for wrapping it up in the context of a saga
		/// </summary>
		/// <typeparam name="T">Type of aggregated root</typeparam>
		/// <param name="saga"><see cref="ISaga"/> to handle in context of</param>
		/// <param name="aggregatedRootId">Id of the <see cref="AggregatedRoot"/></param>
		/// <param name="method">Expression expressing the method on the aggregated root with the arguments it needs</param>
		/// <returns>
		/// Result from the handle.
		/// Within the result one can check if the handling was success or not
		/// </returns>
		CommandResult Handle<T>(ISaga saga, Guid aggregatedRootId, Expression<Action<T>> method) where T : AggregatedRoot;

	    /// <summary>
	    /// Handle a command by using a method as definition on the aggregated root 
	    /// and using a dynamic command for wrapping it up
	    /// </summary>
	    /// <typeparam name="T">Type of aggregated root</typeparam>
	    /// <param name="aggregatedRootId">Id of the <see cref="AggregatedRoot"/></param>
	    /// <param name="method">Expression expressing the method on the aggregated root with the arguments it needs</param>
	    /// <returns>
	    /// Result from the handle.
	    /// Within the result one can check if the handling was success or not
	    /// </returns>
	    CommandResult Handle<T>(Guid aggregatedRootId, Expression<Action<T>> method) where T : AggregatedRoot;
	}
}
