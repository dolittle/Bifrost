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
using Bifrost.Commands;

namespace Bifrost.Domain
{
	/// <summary>
	/// Represents a <see cref="IAggregatedRootFactory{T}">IAggregatedRootFactory</see>
	/// </summary>
	/// <typeparam name="T">Type of aggregated root the factory is for</typeparam>
	public class AggregatedRootFactory<T> : IAggregatedRootFactory<T>
		where T : AggregatedRoot
	{
		readonly ICommandContextManager _commandContextManager;

		/// <summary>
		/// Initializes a new instance of <see cref="AggregatedRootFactory{T}">AggregatedRootFactory</see>
		/// </summary>
		/// <param name="commandContextManager">
		/// A <see cref="ICommandContextManager">ICommandContextManager</see> for registering objects for tracking
		/// during creation
		/// </param>
		public AggregatedRootFactory(ICommandContextManager commandContextManager)
		{
			_commandContextManager = commandContextManager;
		}

#pragma warning disable 1591 // Xml Comments
		public T Create(Guid id)
		{
			var aggregatedRoot = Activator.CreateInstance(typeof(T), id) as T;
			var commandContext = _commandContextManager.GetCurrent();
			commandContext.RegisterForTracking(aggregatedRoot);
			return aggregatedRoot;
		}

        object IAggregatedRootFactory.Create(Guid id)
        {
            return Create(id);
        }
#pragma warning restore 1591 // Xml Comments
	}
}