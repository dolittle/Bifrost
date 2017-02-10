/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;

namespace Bifrost.Strings
{
    /// <summary>
    /// Represents an implementation of <see cref="IFixedStringSegmentBuilder"/>
    /// </summary>
    public class FixedStringSegmentBuilder : IFixedStringSegmentBuilder
    {
        string _string;
        bool _optional;
        SegmentOccurrence _occurences;
        bool _dependingOnPrevious;
        ISegmentBuilder _parent;
        List<ISegmentBuilder> _children = new List<ISegmentBuilder>();


        /// <summary>
        /// Initializes a new instance of <see cref="FixedStringSegmentBuilder"/>
        /// </summary>
        /// <param name="string">Fixed string we're building for</param>
        /// <param name="parent">Parent <see cref="ISegmentBuilder"/></param>
        /// <param name="children"><see cref="IEnumerable{ISegmentBuilder}">Children</see></param>
        /// <param name="optional">Wether or not its optional</param>
        /// <param name="occurrences">Number of occurrences</param>
        /// <param name="dependingOnPrevious">Wether or not it depends on previous segment</param>
        public FixedStringSegmentBuilder(
            string @string,
            ISegmentBuilder parent,
            IEnumerable<ISegmentBuilder> children,
            bool optional=false, 
            SegmentOccurrence occurrences=SegmentOccurrence.Single, 
            bool dependingOnPrevious=false)
        {
            _string = @string;
            _optional = optional;
            _occurences = occurrences;
            _dependingOnPrevious = dependingOnPrevious;
            _parent = parent;
            _children = new List<ISegmentBuilder>(children);
        }

        /// <inheritdoc/>
        public bool HasChildren { get { return _children.Count() > 0; } }

        /// <inheritdoc/>
        public bool DependsOnPrevious => _dependingOnPrevious;

        /// <inheritdoc/>
        public FixedStringSegment Build()
        {
            var segment = new FixedStringSegment(
                @string:_string, 
                optional:_optional, 
                occurrences:_occurences,
                parent:_parent?.Build(),
                children:_children.Select(s=>s.Build())
                );
            return segment;
        }

        /// <inheritdoc/>
        public IFixedStringSegmentBuilder DependingOnPrevious()
        {
            _parent.AddChild(this);
            return new FixedStringSegmentBuilder(
                @string: _string,
                parent: _parent,
                children: _children,
                optional: _optional,
                occurrences: _occurences,
                dependingOnPrevious: true);
        }

        /// <inheritdoc/>
        public IFixedStringSegmentBuilder Optional()
        {
            return new FixedStringSegmentBuilder(
                @string:_string,
                parent: _parent,
                children: _children,
                optional:true, 
                occurrences:_occurences,
                dependingOnPrevious:_dependingOnPrevious);
        }

        /// <inheritdoc/>
        public IFixedStringSegmentBuilder Recurring()
        {
            return new FixedStringSegmentBuilder(
                @string: _string,
                parent: _parent,
                children: _children,
                optional: _optional,
                occurrences: SegmentOccurrence.Recurring,
                dependingOnPrevious: _dependingOnPrevious);
        }

        /// <inheritdoc/>
        public IFixedStringSegmentBuilder Single()
        {
            return new FixedStringSegmentBuilder(
                @string: _string,
                parent: _parent,
                children: _children,
                optional: _optional,
                occurrences: SegmentOccurrence.Single,
                dependingOnPrevious: _dependingOnPrevious);
        }

        /// <inheritdoc/>
        public void AddChild(ISegmentBuilder child)
        {
            _children.Add(child);
        }


        ISegment ISegmentBuilder.Build()
        {
            return Build();
        }

    }
}
