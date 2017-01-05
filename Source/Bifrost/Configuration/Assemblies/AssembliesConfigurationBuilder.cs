/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Configuration.Assemblies
{
    /// <summary>
    /// Represents a builder for building configuration used by <see cref="Assemblies"/>
    /// </summary>
    public class AssembliesConfigurationBuilder
    {
        /// <summary>
        /// Gets the <see cref="IAssemblyRuleBuilder">rule builder</see> used
        /// </summary>
        public IAssemblyRuleBuilder RuleBuilder { get; private set; }


        /// <summary>
        /// Include all assemblies with possible exceptions
        /// </summary>
        /// <returns>
        /// Returns the <see cref="IncludeAll">configuration object</see> for the rule
        /// </returns>
        public IncludeAll IncludeAll()
        {
            var includeAll = new IncludeAll();
            RuleBuilder = includeAll;
            return includeAll;
        }
    }
}
