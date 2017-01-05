/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Web;

namespace Bifrost.Web.Services
{
    /// <summary>
    /// Defines a factory for building a <see cref="RequestParams">Request Parameters collection </see>
    /// </summary>
    public interface IRequestParamsFactory
    {
        /// <summary>
        /// Builds an instance of <see cref="RequestParams">RequestParams</see> that encapsulates request parameters
        /// </summary>
        /// <param name="request">An HttpRequestBase instance</param>
        /// <returns></returns>
        RequestParams BuildParamsCollectionFrom(HttpRequestBase request);
    }
}