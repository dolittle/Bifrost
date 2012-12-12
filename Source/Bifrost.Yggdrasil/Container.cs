
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
