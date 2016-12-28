#region License
//
// Copyright (c) 2008-2015, Dolittle (http://www.dolittle.com)
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

namespace Bifrost.Events
{
    /// <summary>
    /// Represents an exception situation where a <see cref="EventMigrationHierarchy">EventMigrationHierarchy</see> is
    /// asked for a concrete type at a level that does not exist.
    /// 
    /// This could be a level less than 0, or a level greater than the hierarchy depth.
    /// </summary>
    public class MigrationLevelOutOfRangeException : Exception
    {
        /// <summary>
        /// Initializes a <see cref="MigrationLevelOutOfRangeException">MigrationLevelOutOfRangeException</see>
        /// </summary>
        public MigrationLevelOutOfRangeException()
        {}

        /// <summary>
        /// Initializes a <see cref="MigrationLevelOutOfRangeException">MigrationLevelOutOfRangeException</see>
        /// </summary>
        /// <param name="message">Error Message</param>
        public MigrationLevelOutOfRangeException(string message) : base(message)
        {}

        /// <summary>
        /// Initializes a <see cref="MigrationLevelOutOfRangeException">MigrationLevelOutOfRangeException</see>
        /// </summary>
        /// <param name="message">Error Message</param>
        /// <param name="innerException">Inner Exception</param>
        public MigrationLevelOutOfRangeException(string message, Exception innerException) : base(message,innerException)
        { }
    }
}