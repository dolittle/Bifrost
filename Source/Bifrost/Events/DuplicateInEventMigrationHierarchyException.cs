/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
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