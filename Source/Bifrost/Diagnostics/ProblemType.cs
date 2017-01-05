/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Diagnostics
{
    /// <summary>
    /// Represents a type of problem, its underlying type is an identifier that should be unique per <see cref="ProblemType">problem type</see>
    /// </summary>
    public class ProblemType
    {
        /// <summary>
        /// Gets the unique identifier for the <see cref="ProblemType"/>
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets the description of the problem type
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Gets the severity of the problem
        /// </summary>
        public ProblemSeverity Severity { get; private set; }

        /// <summary>
        /// Creates a new <see cref="ProblemType"/> from a given id and description
        /// </summary>
        /// <param name="id">Id in the form of a valid <see cref="Guid"/> string representation</param>
        /// <param name="description">Description of the <see cref="ProblemType">problem type</see></param>
        /// <param name="severity">The <see cref="ProblemSeverity">severity</see> of the problem type</param>
        /// <returns>An <see cref="ProblemType"/> instance</returns>
        public static ProblemType Create(string id, string description, ProblemSeverity severity)
        {
            return new ProblemType { Id = Guid.Parse(id), Description = description, Severity = severity };
        }
    }
}
