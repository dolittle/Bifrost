/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Specialized;

namespace Bifrost.Web.Services
{
    public interface IRestServiceMethodInvoker
    {
        string Invoke(string baseUrl, object instance, Uri uri, NameValueCollection form);
    }
}
