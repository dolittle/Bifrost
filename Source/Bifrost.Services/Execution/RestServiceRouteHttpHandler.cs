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
using Bifrost.Serialization;
using Microsoft.Practices.ServiceLocation;

namespace Bifrost.Services.Execution
{
    // Todo : add async support - performance gain! 
    public class RestServiceRouteHttpHandler : IHttpHandler, IRequiresSessionState // IHttpAsyncHandler
    {
        Type _type;
        string _url;

        public RestServiceRouteHttpHandler(Type type, string url)
        {
            _type = type;
            _url = url;
        }

        public bool IsReusable { get { return true; } }

        public void ProcessRequest(HttpContext context)
        {
            var form = context.Request.Form;

            if (form.Keys.Count == 0 && context.Request.InputStream.Length > 0)
            {
                var input = new byte[context.Request.InputStream.Length];
                context.Request.InputStream.Read(input, 0, input.Length);

                var inputDictionary = new Dictionary<string, string>();
                var inputAsString = System.Text.UTF8Encoding.UTF8.GetString(input);
                var serializer = ServiceLocator.Current.GetInstance<ISerializer>();
                serializer.FromJson(inputDictionary, inputAsString);

                form = new NameValueCollection();
                foreach (var key in inputDictionary.Keys)
                    form[key] = inputDictionary[key];
            }
            
            var invoker = ServiceLocator.Current.GetInstance<IRestServiceMethodInvoker>();
            var serviceInstance = ServiceLocator.Current.GetInstance(_type);

            invoker.Invoke(_url, serviceInstance, context.Request.Url, form);
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
