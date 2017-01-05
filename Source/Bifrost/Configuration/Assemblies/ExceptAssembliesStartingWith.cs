/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Linq;
using Bifrost.Specifications;

namespace Bifrost.Configuration.Assemblies
{
    /// <summary>
    /// Rule representing an exception for <see cref="IncludeAllRule"/>, 
    /// excluding assembies starting with
    /// </summary>
    public class ExceptAssembliesStartingWith : Specification<string>
    {
        /// <summary>
        /// Initializes an instance of <see cref="ExceptAssembliesStartingWith"/>
        /// </summary>
        /// <param name="names"></param>
        public ExceptAssembliesStartingWith(params string[] names)
        {
            Predicate = a => !names.Any(n => a.StartsWith(n));
        }
    }
}
