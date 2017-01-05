/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
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