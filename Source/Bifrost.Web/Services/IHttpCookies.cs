/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Bifrost.Web.Services
{
    /// <summary>
    /// Defines a collection of <see cref="IHttpCookie"/>
    /// </summary>
    public interface IHttpCookies : IDictionary<string, IHttpCookie>
    {
        /// <summary>
        /// Get a <see cref="IHttpCookie"/> by index
        /// </summary>
        /// <param name="index">Index to get</param>
        /// <returns>A <see cref="IHttpCookie"/></returns>
        IHttpCookie Get(int index);
    }
}