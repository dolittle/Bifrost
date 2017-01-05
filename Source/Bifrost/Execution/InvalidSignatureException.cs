/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Reflection;

namespace Bifrost.Execution
{
    /// <summary>
    /// Exception that is thrown when signature of a method does not match
    /// how it is called. Typically used when dynamically invoking a <see cref="WeakDelegate"/>
    /// </summary>
    public class InvalidSignatureException : ArgumentException
    {
        /// <summary>
        /// Initialzes a new instance of <see cref="InvalidSignatureException"/>
        /// </summary>
        /// <param name="expectedSignature"><see cref="MethodInfo"/> that represents the expected signature</param>
        public InvalidSignatureException(MethodInfo expectedSignature) : base(string.Format("Method '{0}' was invoked with the wrong signature, expected: {1}", expectedSignature.Name, expectedSignature.ToString()))
        {
        }
    }
}
