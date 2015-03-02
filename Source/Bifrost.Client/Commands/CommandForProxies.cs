#region License
//
// Copyright (c) 2008-2013, Dolittle (http://www.dolittle.com)
//
// Licensed under the MIT License (http://opensource.org/licenses/MIT)
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at 
//
//   http://github.com/dolittle/Bifrost/blob/master/MIT-LICENSE.txt
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion
using System;
using System.ComponentModel;
using System.Diagnostics;
using Bifrost.Execution;
using Castle.DynamicProxy;

namespace Bifrost.Commands
{
    /// <summary>
    /// Represents an implementation of <see cref="ICommandForProxies"/>
    /// </summary>
    [Singleton]
    public class CommandForProxies : ICommandForProxies
    {
        ProxyGenerator _proxyGenerator;

        /// <summary>
        /// Initializes a new instance of <see cref="CommandForProxies"/>
        /// </summary>
        public CommandForProxies()
        {
            _proxyGenerator = new ProxyGenerator();
        }


#pragma warning disable 1591 // Xml Comments


        public class MyInterceptor : IInterceptor
        {

            public void Intercept(IInvocation invocation)
            {
                var get_Instance = typeof(ICommandFor<>).GetProperty("Instance").GetGetMethod();
                if (invocation.Method == get_Instance)
                {
                    Debug.WriteLine("Instance");
                }

                if (invocation.Method.Name.Contains("Instance"))
                {
                    Debug.WriteLine("Method : " + invocation.Method.Name);

                    //invocation.ReturnValue = "HEllo world";
                }

                var i = 0;
                i++;
                
            }
        }

        public ICommandFor<T> GetFor<T>() where T : ICommand
        {
            var instance = _proxyGenerator.CreateInterfaceProxyWithoutTarget(typeof(ICommandFor<T>), new MyInterceptor()) as ICommandFor<T>;

            var i = instance.Instance;

            return instance;
        }
#pragma warning restore 1591 // Xml Comments
    }
}
