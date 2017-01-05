/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;

namespace Bifrost.Diagnostics
{
    /// <summary>
    /// Represents an implementation of <see cref="IProblems"/>
    /// </summary>
    public class Problems : IProblems
    {
        List<Problem> _problems = new List<Problem>();

#pragma warning disable 1591 // Xml Comments
        public void Report(ProblemType type, object data)
        {
            _problems.Add(new Problem
            {
                Type = type,
                Data = data
            });
        }

        public bool Any { get { return _problems.Count > 0; } }
 
        public IEnumerator<Problem> GetEnumerator()
        {
            return _problems.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _problems.GetEnumerator();
        }
#pragma warning restore 1591 // Xml Comments
    }
}
