#region License
//
// Copyright (c) 2008-2012, DoLittle Studios AS and Komplett ASA
//
// Licensed under the Microsoft Permissive License (Ms-PL), Version 1.1 (the "License")
// With one exception :
//   Commercial libraries that is based partly or fully on Bifrost and is sold commercially, 
//   must obtain a commercial license.
//
// You may not use file except in compliance with the License.
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
