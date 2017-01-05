/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Diagnostics
{
    /// <summary>
    /// Defines a rule that runs diagnostics on a specific type
    /// </summary>
    /// <typeparam name="T">Type that the rule applies to</typeparam>
    public interface ITypeRuleFor<T>
    {
        /// <summary>
        /// Validate and report any problems
        /// </summary>
        /// <param name="type"><see cref="Type"/> to validate</param>
        /// <param name="problems"><see cref="Problems"/> to report back on, if any</param>
        void Validate(Type type, IProblems problems);
    }
}
