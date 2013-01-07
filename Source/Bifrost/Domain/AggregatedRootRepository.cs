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
using System.Collections.Generic;
using System.Linq;
using Bifrost.Commands;
using Bifrost.Events;

namespace Bifrost.Domain
{
	/// <summary>
	/// Defines a concrete implementation of <see cref="IAggregatedRootRepository{T}">IAggregatedRootRepository</see>
	/// </summary>
	/// <typeparam name="T">Type the repository is for</typeparam>
	public class AggregatedRootRepository<T> : IAggregatedRootRepository<T>
		where T : AggregatedRoot
	{
		readonly ICommandContextManager _commandContextManager;

		/// <summary>
		/// Initializes a new instance of <see cref="AggregatedRootRepository{T}">AggregatedRootRepository</see>
		/// </summary>
		/// <param name="commandContextManager"> <see cref="ICommandContextManager"/> to use for tracking </param>
		public AggregatedRootRepository(ICommandContextManager commandContextManager)
		{
			_commandContextManager = commandContextManager;
		}

#pragma warning disable 1591 // Xml Comments
		public T Get(Guid id)
		{
			var commandContext = _commandContextManager.GetCurrent();
			var type = typeof (T);
			var aggregatedRoot = Activator.CreateInstance(type, id) as T;
			if (null != aggregatedRoot)
			{
                if(!aggregatedRoot.IsStateless())
                {
                    foreach (var stream in commandContext.EventStores.Select(eventStore => eventStore.GetForEventSource(aggregatedRoot, id)).Where(stream => stream.HasEvents))
                    {
                        aggregatedRoot.ReApply(stream);
                    }  
                }
                else
                {
                    var versions = commandContext.EventStores.Select(eventStore => eventStore.GetLastCommittedVersion(aggregatedRoot, id)).ToList();

                    aggregatedRoot.FastForward(versions.Max());
                }
			}
            commandContext.RegisterForTracking(aggregatedRoot);

			return aggregatedRoot;
		}

        object IAggregatedRootRepository.Get(Guid id)
        {
            return Get(id);
        }
#pragma warning restore 1591 // Xml Comments
	}
}