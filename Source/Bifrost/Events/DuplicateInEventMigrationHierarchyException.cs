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
    /// Represents an exceptional situation where an event in an <see cref="EventMigrationHierarchy">EventMigrationHierarchy</see>
    /// has more than one migration path.
    /// </summary>
    public class DuplicateInEventMigrationHierarchyException : Exception
    {
		/// <summary>
		/// Initializes an instance of <see cref="DuplicateInEventMigrationHierarchyException"/>
		/// </summary>
        public DuplicateInEventMigrationHierarchyException()
        {}

		/// <summary>
		/// Initializes an instance of <see cref="DuplicateInEventMigrationHierarchyException"/>
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception</param>
		public DuplicateInEventMigrationHierarchyException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// Initializes an instance of <see cref="DuplicateInEventMigrationHierarchyException"/>
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception</param>
		/// <param name="innerException">The inner exception that is causing the exception</param>
		public DuplicateInEventMigrationHierarchyException(string message, Exception innerException)
			: base(message, innerException)
        { }
    }
}