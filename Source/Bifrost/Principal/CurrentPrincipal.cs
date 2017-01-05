/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace Bifrost.Principal
{
    /// <summary>
    /// Represents the current principal.
    /// Thread.CurrentPrincipal substituted for explicit Principal for testing purposes.
    /// </summary>
    public class CurrentPrincipal : IDisposable
    {
        /// <summary>
        /// Gets the minimum time supported by the <see cref="SystemClock"/>
        /// </summary>
        public static readonly DateTime MinimumTime = new DateTime(1900, 1, 1);

        static readonly Stack<IPrincipal> _principals = new Stack<IPrincipal>();

        /// <summary>
        /// Retrieves the current principal
        /// </summary>
        /// <returns>Principal</returns>
        public static IPrincipal Get()
        {
            if (_principals.Count > 0)
                return _principals.Peek();

            return GenericPrincipal.Current;
        }

        /// <summary>
        /// Allows the current principal to be set to an explicit value.  SOLELY FOR TESTING PURPOSES.
        /// Use within a "using" block within your spec so that the current principal is reset on exiting..
        /// </summary>
        /// <param name="principal">The explicit principal that you wish to set within the spec</param>
        /// <returns>A new instance of  CurrentPrincipal </returns>
        public static IDisposable SetPrincipalTo(IPrincipal principal)
        {
            _principals.Push(principal);
            return new CurrentPrincipal();
        }

        /// <summary>
        /// Will remove any explicitly set current time,
        /// </summary>
        public void Dispose()
        {
            if(_principals.Count > 0)
                _principals.Pop();
        }
    };
}