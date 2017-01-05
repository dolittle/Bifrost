/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Specifications;

namespace Bifrost.Configuration.Assemblies
{
    /// <summary>
    /// Represents the <see cref="IAssemblyRuleBuilder">builder</see> for building the <see cref="IncludeAllRule"/> and
    /// possible exceptions
    /// </summary>
    public class IncludeAll : IAssemblyRuleBuilder
    {
        /// <summary>
        /// Initializes an instance of <see cref="IncludeAll"/>
        /// </summary>
        public IncludeAll()
        {
            Specification = new IncludeAllRule();
        }

        /// <summary>
        /// Gets the <see cref="IncludeAllRule"/>
        /// </summary>
        public Specification<string> Specification { get; set; }
    }
}
