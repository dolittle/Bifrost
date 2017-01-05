/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Bifrost.Execution;

namespace Bifrost.Diagnostics
{
    /// <summary>
    /// Represents an implementation of <see cref="IProblemsReporter"/>
    /// </summary>
    [Singleton]
    public class ProblemsReporter : IProblemsReporter
    {
        List<IProblems> _allProblems = new List<IProblems>();

#pragma warning disable 1591 // Xml Comments
        public void Clear()
        {
            _allProblems.Clear();
        }

        public void Report(IProblems problems)
        {
            _allProblems.Add(problems);
        }

        public IEnumerable<IProblems> All { get { return _allProblems;  } }
#pragma warning restore 1591 // Xml Comments

    }
}
