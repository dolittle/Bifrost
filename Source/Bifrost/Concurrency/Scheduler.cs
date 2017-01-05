/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;

namespace Bifrost.Concurrency
{
    /// <summary>
    /// Represents a <see cref="IScheduler"/>
    /// </summary>
    public class Scheduler : IScheduler
    {
        Dictionary<Guid, CancellationTokenSource> _cancellationTokens = new Dictionary<Guid, CancellationTokenSource>();


#pragma warning disable 1591 // Xml Comments
        public Guid Start(Action action, Action actionDone = null)
        {
            var id = Guid.NewGuid();
            var cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSource.Token;
            _cancellationTokens[id] = cancellationTokenSource;

            var task = Task.Factory.StartNew(action, cancellationToken);
            task.ContinueWith(t =>
            {
                _cancellationTokens.Remove(id);
                if (actionDone != null)
                    actionDone();
            });

            return id;
        }

        public Guid Start<T>(Action<T> action, T objectState, Action<T> actionDone = null)
        {
            var id = Guid.NewGuid();
            var cancellationTokenSource = new CancellationTokenSource();
            //var cancellationToken = cancellationTokenSource.Token;
            _cancellationTokens[id] = cancellationTokenSource;

            var task = Task.Factory.StartNew(o=>action((T)o), objectState);

            task.ContinueWith(t =>
            {
                _cancellationTokens.Remove(id);
                if (actionDone != null)
                    actionDone(objectState);
            });

            return id;
        }

        public void Stop(Guid id)
        {
            if (_cancellationTokens.ContainsKey(id))
                _cancellationTokens[id].Cancel();
        }
#pragma warning restore 1591 // Xml Comments
    }
}
