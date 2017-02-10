/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;

namespace Bifrost.Strings
{
    /// <summary>
    /// Represents an implementation of <see cref="IVariableStringSegmentBuilder"/>
    /// </summary>
    public class VariableStringSegmentBuilder : IVariableStringSegmentBuilder
    {
        string _variableName;
        bool _optional;
        SegmentOccurrence _occurences;
        bool _dependingOnPrevious;
        ISegmentBuilder _parent;
        List<ISegmentBuilder> _children = new List<ISegmentBuilder>();


        /// <summary>
        /// Initializes a new instance of <see cref="VariableStringSegmentBuilder"/>
        /// </summary>
        /// <param name="variableName">Variable string we're building for</param>
        /// <param name="parent">Parent <see cref="ISegmentBuilder"/></param>
        /// <param name="children"><see cref="IEnumerable{ISegmentBuilder}">Children</see></param>
        /// <param name="optional">Wether or not its optional</param>
        /// <param name="occurrences">Number of occurrences</param>
        /// <param name="dependingOnPrevious">Wether or not it depends on previous segment</param>
        public VariableStringSegmentBuilder(
            string variableName,
            ISegmentBuilder parent,
            IEnumerable<ISegmentBuilder> children,
            bool optional=false, 
            SegmentOccurrence occurrences=SegmentOccurrence.Single, 
            bool dependingOnPrevious=false)
        {
            _variableName = variableName;
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
        public VariableStringSegment Build()
        {
            var segment = new VariableStringSegment(
                variableName:_variableName, 
                optional:_optional, 
                occurrences:_occurences,
                parent:_parent?.Build(),
                children:_children.Select(s=>s.Build())
                );
            return segment;
        }

        /// <inheritdoc/>
        public IVariableStringSegmentBuilder DependingOnPrevious()
        {
            _parent.AddChild(this);
            return new VariableStringSegmentBuilder(
                variableName: _variableName,
                parent: _parent,
                children: _children,
                optional: _optional,
                occurrences: _occurences,
                dependingOnPrevious: true);
        }

        /// <inheritdoc/>
        public IVariableStringSegmentBuilder Optional()
        {
            return new VariableStringSegmentBuilder(
                variableName: _variableName,
                parent: _parent,
                children: _children,
                optional:true, 
                occurrences:_occurences,
                dependingOnPrevious:_dependingOnPrevious);
        }

        /// <inheritdoc/>
        public IVariableStringSegmentBuilder Recurring()
        {
            return new VariableStringSegmentBuilder(
                variableName: _variableName,
                parent: _parent,
                children: _children,
                optional: _optional,
                occurrences: SegmentOccurrence.Recurring,
                dependingOnPrevious: _dependingOnPrevious);
        }

        /// <inheritdoc/>
        public IVariableStringSegmentBuilder Single()
        {
            return new VariableStringSegmentBuilder(
                variableName: _variableName,
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
