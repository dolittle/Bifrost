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
using System.Linq;
using System.ServiceModel;
using System.Web.Routing;

namespace Bifrost.Services.Execution
{
    public static class ServiceRoutingExtensions
    {
        const string PostFix = "Service";

        public static void AddService<T>(this RouteCollection routes, string url = null, bool removePostFix = true)
        {
            routes.AddService(typeof(T), url, removePostFix);
        }

        public static void AddService(this RouteCollection routes, Type service, string url = null, bool removePostFix = true)
        {
            url = url ?? (removePostFix ? RemovePostFix(service) : service.Name);

            routes.Add(new RestServiceRoute(service, url));

            //routes.Add(new WebApiRoute(url, new ContainerServiceHostFactory(), service));
        }


        static string RemovePostFix(this Type serviceType)
        {
            var name = serviceType.Name;
            if (name.EndsWith(PostFix))
                name = name.Substring(0, name.Length - PostFix.Length);

            return name;
        }

        static bool IsService(this Type type)
        {
            return type.GetCustomAttributes(typeof(ServiceContractAttribute), true).Length > 0;
        }

        public static void AddServicesFromNamespaceOf<T>(this RouteCollection routes, bool removePostfix = true)
        {
            var rootType = typeof(T);
            var types = rootType.Assembly.GetTypes().Where(t => t.Namespace != null && t.Namespace.StartsWith(rootType.Namespace) && t.IsService());
            foreach (var type in types)
                routes.AddService(type, removePostfix ? type.RemovePostFix() : null);
        }
    }
}