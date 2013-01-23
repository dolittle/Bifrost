#region License
//
// Copyright (c) 2008-2012, DoLittle Studios AS and Komplett ASA
//
// Licensed under the Microsoft Permissive License (Ms-PL), Version 1.1 (the "License")
// With one exception :
//   Commercial libraries that is based partly or fully on Bifrost and is sold commercially,
//   must obtain a commercial license.
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at
//
//   http://bifrost.codeplex.com/license
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Web.SessionState;
using Bifrost.Configuration;
using Bifrost.Execution;
using Bifrost.Serialization;

namespace Bifrost.Services.Execution
{
    // Todo : add async support - performance gain! 
    public class RestServiceRouteHttpHandler : IHttpHandler, IRequiresSessionState // IHttpAsyncHandler
    {
        Type _type;
        string _url;
        IRequestParamsFactory _factory;
        IRestServiceMethodInvoker _invoker;
        IContainer _container;

        public RestServiceRouteHttpHandler(Type type, string url) 
            : this(
                type,
                url,
                Configure.Instance.Container.Get<IRequestParamsFactory>(),
                Configure.Instance.Container.Get<IRestServiceMethodInvoker>(),
                Configure.Instance.Container)
        {}

        public RestServiceRouteHttpHandler(Type type, string url, IRequestParamsFactory factory, IRestServiceMethodInvoker invoker, IContainer container)
        {
            _type = type;
            _url = url;
            _factory = factory;
            _invoker = invoker;
            _container = container;
        }

        public bool IsReusable { get { return true; } }

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                var form = _factory.BuildParamsCollectionFrom(new HttpRequestWrapper(HttpContext.Current.Request));
                var serviceInstance = _container.Get(_type);
                var result = _invoker.Invoke(_url, serviceInstance, context.Request.Url, form);
                context.Response.Write(result);
            }
            catch( Exception e)
            {
                if (e.InnerException != null && e.InnerException is HttpStatus.HttpStatusException)
                {
                    var ex = e.InnerException as HttpStatus.HttpStatusException;
                    context.Response.StatusCode = ex.Code;
                    context.Response.StatusDescription = ex.Description;
                }
                else
                {
                    context.Response.StatusCode = 500;
                    context.Response.StatusDescription = e.Message.Substring(0,e.Message.Length >= 512 ? 512: e.Message.Length);
                }
            }
        }

        /*
        public IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData)
        {
            ProcessRequest(context);

            throw new System.NotImplementedException();
        }

        public void EndProcessRequest(IAsyncResult result)
        {
            throw new System.NotImplementedException();
        }*/
    }
}
