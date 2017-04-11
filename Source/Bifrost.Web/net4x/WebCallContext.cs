/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Web;
using Bifrost.Execution;

namespace Bifrost.Web
{
    public class WebCallContext : ICallContext
    {
        public bool HasData(string key)
        {
            return HttpContext.Current.Items.Contains(key);
        }

        public T GetData<T>(string key)
        {
            return (T)HttpContext.Current.Items[key];
        }

        public void SetData(string key, object data)
        {
            HttpContext.Current.Items[key] = data;
        }
    }
}
