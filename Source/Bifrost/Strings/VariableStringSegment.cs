/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;

namespace Bifrost.Strings
{
    /// <summary>
    /// Represents a <see cref="ISegment"/> for an arbitrary string
    /// </summary>
    public class VariableStringSegment : Segment
    {
        /// <summary>
        /// Initializes a new instance <see cref="FixedStringSegment"/>
        /// </summary>
        /// <param name="variableName">The variable name to expect</param>
        /// <param name="optional">Wether or not if the segment is optional</param>
        /// <param name="occurrences">Occurrences of the segment</param>
        /// <param name="parent">Parent <see cref="ISegment"/></param>
        /// <param name="children"><see cref="IEnumerable{ISegment}">Children</see></param>
        public VariableStringSegment(
            string variableName, 
            bool optional, 
            SegmentOccurence occurrences, 
            ISegment parent, 
            IEnumerable<ISegment> children)
        {
            VariableName = variableName;
            Optional = optional;
            Occurrences = occurrences;
            Parent = parent;
            Children = children;
        }

        /// <inheritdoc/>
        public override bool Fixed => false;

        /// <summary>
        /// Gets the expected <see cref="string"/>
        /// </summary>
        public string VariableName { get; }

        /// <inheritdoc/>
        public override ISegmentMatch Match(IEnumerable<string> input)
        {
            throw new NotImplementedException();
        }
    }
}
