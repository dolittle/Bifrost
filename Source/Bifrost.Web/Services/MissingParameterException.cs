/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Web.Services
{
    public class MissingParameterException : ArgumentException
    {
        public MissingParameterException(string parameter, string serviceName, Uri uri) : 
            base(string.Format("Parameter '{0}' is missing for '{1}' in the request '{2}",parameter, serviceName, uri))
        {
        }
    }
}
