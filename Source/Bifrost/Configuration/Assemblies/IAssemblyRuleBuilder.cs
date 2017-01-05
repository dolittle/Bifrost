/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Specifications;

namespace Bifrost.Configuration.Assemblies
{
    /// <summary>
    /// Defines a rule builder for building configuration for assemblies and how to include
    /// or exclude assemblies
    /// </summary>
    public interface IAssemblyRuleBuilder
    {
        /// <summary>
        /// Get the specification to use
        /// </summary>
        Specification<string> Specification { get; set; }
    }
}
