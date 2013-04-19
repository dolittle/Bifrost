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
using Bifrost.Execution;
namespace Bifrost.Yggdrasil
{
    public class Container : IContainer
    {
        Yggdrasil.Container _container;

        public Container(Yggdrasil.Container container)
        {
            _container = container;
        }


        public T Get<T>()
        {
            throw new System.NotImplementedException();
        }

        public T Get<T>(bool optional)
        {
            throw new System.NotImplementedException();
        }

        public object Get(System.Type type)
        {
            throw new System.NotImplementedException();
        }

        public object Get(System.Type type, bool optional = false)
        {
            throw new System.NotImplementedException();
        }

        public System.Collections.Generic.IEnumerable<T> GetAll<T>()
        {
            throw new System.NotImplementedException();
        }

        public bool HasBindingFor(System.Type type)
        {
            throw new System.NotImplementedException();
        }

        public bool HasBindingFor<T>()
        {
            throw new System.NotImplementedException();
        }

        public System.Collections.Generic.IEnumerable<object> GetAll(System.Type type)
        {
            throw new System.NotImplementedException();
        }

        public System.Collections.Generic.IEnumerable<System.Type> GetBoundServices()
        {
            throw new System.NotImplementedException();
        }

        public void Bind(System.Type service, System.Func<System.Type> resolveCallback)
        {
            throw new System.NotImplementedException();
        }

        public void Bind<T>(System.Func<System.Type> resolveCallback)
        {
            throw new System.NotImplementedException();
        }

        public void Bind(System.Type service, System.Func<System.Type> resolveCallback, BindingLifecycle lifecycle)
        {
            throw new System.NotImplementedException();
        }

        public void Bind<T>(System.Func<System.Type> resolveCallback, BindingLifecycle lifecycle)
        {
            throw new System.NotImplementedException();
        }

        public void Bind<T>(System.Type type)
        {
            throw new System.NotImplementedException();
        }

        public void Bind(System.Type service, System.Type type)
        {
            throw new System.NotImplementedException();
        }

        public void Bind<T>(System.Type type, BindingLifecycle lifecycle)
        {
            throw new System.NotImplementedException();
        }

        public void Bind(System.Type service, System.Type type, BindingLifecycle lifecycle)
        {
            throw new System.NotImplementedException();
        }

        public void Bind<T>(T instance)
        {
            throw new System.NotImplementedException();
        }

        public void Bind(System.Type service, object instance)
        {
            throw new System.NotImplementedException();
        }
    }
}
