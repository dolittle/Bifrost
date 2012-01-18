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
using System.Collections.Specialized;
using Bifrost.Serialization;

namespace Bifrost.Services.Execution
{
    public class RestServiceMethodInvoker : IRestServiceMethodInvoker
    {
        ISerializer _serializer;

        public RestServiceMethodInvoker(ISerializer serializer)
        {
            _serializer = serializer;
        }

        public string Invoke(string baseUrl, object instance, Uri uri, NameValueCollection form)
        {
            var methodName = GetMethodNameFromUri(baseUrl, uri);
            ThrowIfMethodNameNotSpecified(methodName, instance, uri);
            ThrowIfMethodMissing(methodName, instance, uri);

            var method = instance.GetType().GetMethod(methodName);
            method.Invoke(instance, new object[0]);

            return string.Empty;
        }

        string GetMethodNameFromUri(string baseUrl, Uri uri)
        {
            var path = uri.AbsolutePath;
            if (path.StartsWith("/"))
                path = path.Substring(1);

            var segments = path.Split('/');
            if (segments.Length > 1)
                return segments[1];

            return string.Empty;
        }

        void ThrowIfMethodNameNotSpecified(string methodName, object instance, Uri uri)
        {
            if (string.IsNullOrEmpty(methodName))
                throw new MethodNotSpecifiedException(instance.GetType(), uri);
        }

        void ThrowIfMethodMissing(string methodName, object instance, Uri uri)
        {
            var method = instance.GetType().GetMethod(methodName);
            if (method == null)
                throw new MissingMethodException(string.Format("Missing method '{0}' for Uri '{1}'", methodName, uri));
        }
    }
}
