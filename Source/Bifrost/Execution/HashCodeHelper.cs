/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Linq;

namespace Bifrost.Execution
{
    /// <summary>
    /// Provides useful methods for dealing with HashCodes
    /// </summary>
    public static class HashCodeHelper
    {
        /// <summary>
        /// Encapsulates an algorithm for generating a hashcode from a series of parameters
        /// </summary>
        /// <param name="parameters">Properties to generate the HashCode from.</param>
        /// <returns>Hash Code</returns>
	    public static int Generate(params object[] parameters)
	    {
            //http://stackoverflow.com/questions/263400/what-is-the-best-algorithm-for-an-overridden-system-object-gethashcode
		    unchecked
		    {
		        return parameters.Where(param => param != null)
                            .Aggregate(17, (current, param) => current*29 + param.GetHashCode());
		    }
	    }
    }
}