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
using Bifrost.Concurrency;

namespace Bifrost.Events
{
    /// <summary>
    /// Represents a <see cref="IUncommittedEventStreamCoordinator"/> that commits asynchronously
    /// </summary>
    public class AsynchronousUncommittedEventStreamCoordinator : IUncommittedEventStreamCoordinator
    {
        UncommittedEventStreamCoordinator _actualCoordinator;
        IScheduler _scheduler;

        /// <summary>
        /// Initializes a new instance of <see cref="AsynchronousUncommittedEventStreamCoordinator"/>
        /// </summary>
        /// <param name="actualCoordinator">The actual <see cref="UncommittedEventStreamCoordinator"/> to be used</param>
        /// <param name="scheduler"><see cref="IScheduler"/> to use for scheduling asynchronous tasks</param>
        public AsynchronousUncommittedEventStreamCoordinator(UncommittedEventStreamCoordinator actualCoordinator, IScheduler scheduler)
        {
            _actualCoordinator = actualCoordinator;
            _scheduler = scheduler;
        }


#pragma warning disable 1591 // Xml Comments
        public void Commit(UncommittedEventStream eventStream)
        {
            _scheduler.Start(() => _actualCoordinator.Commit(eventStream));
        }
#pragma warning restore 1591 // Xml Comments
    }
}
