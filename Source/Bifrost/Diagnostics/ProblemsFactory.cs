/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Bifrost.Diagnostics
{
    /// <summary>
    /// Represents an implementation of <see cref="IProblemsFactory"/>
    /// </summary>
    public class ProblemsFactory : IProblemsFactory
    {
#pragma warning disable 1591 // Xml Comments
        public IProblems Create()
        {
            var problems = new Problems();
            return problems;
        }
#pragma warning restore 1591 // Xml Comments
    }
}
