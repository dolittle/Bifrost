/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
#if(NET461)
using System.Web.Routing;
#endif

namespace Bifrost.Web
{
    public interface IWebContext
    {
        IWebRequest Request { get; }

#if(NET461)        
        RouteCollection Routes { get; }
#endif
        void RewritePath(string path);
        bool HasRouteForCurrentRequest { get; }
    }
}