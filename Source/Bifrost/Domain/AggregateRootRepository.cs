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
using System;
using System.Collections.Generic;
using System.Linq;
using Bifrost.Commands;
using Bifrost.Events;

namespace Bifrost.Domain
{
	/// <summary>
	/// Defines a concrete implementation of <see cref="IAggregateRootRepository{T}">IAggregatedRootRepository</see>
	/// </summary>
	/// <typeparam name="T">Type the repository is for</typeparam>
	public class AggregateRootRepository<T> : IAggregateRootRepository<T>
		where T : AggregateRoot
	{
		readonly ICommandContextManager _commandContextManager;

		/// <summary>
		/// Initializes a new instance of <see cref="AggregateRootRepository{T}">AggregatedRootRepository</see>
		/// </summary>
		/// <param name="commandContextManager"> <see cref="ICommandContextManager"/> to use for tracking </param>
		public AggregateRootRepository(ICommandContextManager commandContextManager)
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
                    var stream = commandContext.GetCommittedEventsFor(aggregatedRoot, id);
                    if( stream.HasEvents )
                        aggregatedRoot.ReApply(stream);
                }
                else
                {
                    var version = commandContext.GetLastCommittedVersion(aggregatedRoot, id);
                    aggregatedRoot.FastForward(version);
                }
			}
            commandContext.RegisterForTracking(aggregatedRoot);

			return aggregatedRoot;
		}

        object IAggregateRootRepository.Get(Guid id)
        {
            return Get(id);
        }
#pragma warning restore 1591 // Xml Comments
	}
}