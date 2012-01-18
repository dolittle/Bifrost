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

namespace Bifrost.Events
{
    /// <summary>
    /// Defines the functionality for a migrator that migrates from an older generation of <see cref="IEvent">Event</see> to a newer generation
    /// </summary>
    /// <typeparam name="TIn">Older generation of the <see cref="IEvent">Event</see> to migrate from</typeparam>
    /// <typeparam name="TOut">Newer generation of the <see cref="IEvent">Event</see> to migrate to</typeparam>
    public interface IEventMigrator<in TIn, out TOut> where TIn : IEvent where TOut : IEvent
    {
        /// <summary>
        /// Migrates from the incoming <see cref="IEvent">Event</see> to the outgoing <see cref="IEvent">Event</see>
        /// </summary>
        /// <param name="source">Older version of the <see cref="IEvent">Event</see></param>
        /// <returns>Newer version of the <see cref="IEvent">Event</see></returns>
        TOut Migrate(TIn source);
    }
}