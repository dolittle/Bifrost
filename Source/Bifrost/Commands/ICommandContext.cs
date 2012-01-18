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
using System.Collections.Generic;
using System.Security.Principal;
using Bifrost.Domain;
using Bifrost.Events;
using Bifrost.Lifecycle;
using Bifrost.Execution;

namespace Bifrost.Commands
{
	/// <summary>
	/// Defines a context for a <see cref="ICommand">command</see> passing through
	/// the system
	/// </summary>
	public interface ICommandContext : IUnitOfWork
	{
		/// <summary>
		/// Gets the <see cref="ICommand">command</see> the context is for
		/// </summary>
		ICommand Command { get; }

		/// <summary>
		/// Gets the <see cref="IExecutionContext"/> for the command
		/// </summary>
		IExecutionContext ExecutionContext { get; }


		/// <summary>
		/// Gets the <see cref="IEventStore">EventStores</see> to use for the <see cref="ICommandContext"/>
		/// </summary>
		IEnumerable<IEventStore> EventStores { get; }

		/// <summary>
		/// Register an aggregated root for tracking
		/// </summary>
		/// <param name="aggregatedRoot">Aggregated root to track</param>
		void RegisterForTracking(IAggregatedRoot aggregatedRoot);

		/// <summary>
		/// Get objects that are being tracked
		/// </summary>
		/// <returns>All tracked objects</returns>
		IEnumerable<IAggregatedRoot> GetObjectsBeingTracked();
	}
}