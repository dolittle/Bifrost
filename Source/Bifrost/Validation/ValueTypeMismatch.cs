/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Validation
{
    /// <summary>
    /// The exception that is thrown if a value coming in is of the wrong type from what is expected in a rule
    /// </summary>
    public class ValueTypeMismatch : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ValueTypeMismatch"/>
        /// </summary>
        /// <param name="expected">Expected type for value</param>
        /// <param name="actual">Actual type for value</param>
        public ValueTypeMismatch(Type expected, Type actual) : base("Expected '"+expected.Name+"' but got '"+actual.Name+"'")
        {

        }
    }
}
