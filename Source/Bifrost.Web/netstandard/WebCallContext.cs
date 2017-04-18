/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Bifrost.Execution;
using System;
using System.Threading;

namespace Bifrost.Web
{
    [Singleton]
    public class WebCallContext : ICallContext
    {
        const string CurrentContextKey = "CurrentHttpContext";

        static AsyncLocal<HttpContext> _httpContext = new AsyncLocal<HttpContext>();

        HttpContext CurrentContext => _httpContext.Value;

        internal static async Task Middleware(HttpContext context, Func<Task> next)
        {
            _httpContext.Value = context;
            try
            {
                await next();
            }
            finally
            {
            }
        }

        public bool HasData(string key)
        {
            return CurrentContext.Items.ContainsKey(key);
        }

        public T GetData<T>(string key)
        {
            return (T)CurrentContext.Items[key];
        }

        public void SetData(string key, object data)
        {
            CurrentContext.Items[key] = data;
        }
    }
}
