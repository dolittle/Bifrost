/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Security;

namespace Bifrost.Services
{
    /// <summary>
    /// Represents a specific <see cref="ISecurityTarget"/> for services
    /// </summary>
    public class ServiceSecurityTarget : SecurityTarget
    {
        const string SERVICE = "Service";

        /// <summary>
        /// Instantiates an instance of <see cref="ServiceSecurityTarget"/>
        /// </summary>
        public ServiceSecurityTarget() : base(SERVICE)
        {
        }
    }
}
