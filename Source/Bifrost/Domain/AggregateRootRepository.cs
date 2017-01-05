/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
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
#pragma warning restore 1591 // Xml Comments
	}
}