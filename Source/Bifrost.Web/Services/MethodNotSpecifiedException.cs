/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Web.Services
{
    public class MethodNotSpecifiedException : ArgumentException
    {
        public MethodNotSpecifiedException(Type service, Uri uri) : base(string.Format("Method not specified for service invocation for type '{0}' with Uri '{1}",service.AssemblyQualifiedName, uri))
        {
        }
    }
}
