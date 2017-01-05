/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Specifications;

namespace Bifrost.Configuration.Assemblies
{
    /// <summary>
    /// Represents a <see cref="Specification{T}">rule</see> specific to <see cref="Assembly">assemblies</see> 
    /// and used for the <see cref="Assemblies"/>
    /// </summary>
    public class IncludeAllRule : Specification<string>
    {
        /// <summary>
        /// Initializes an instance of <see cref="IncludeAllRule"/>
        /// </summary>
        public IncludeAllRule()
        {
            Predicate = a => true;
        }
    }
}
