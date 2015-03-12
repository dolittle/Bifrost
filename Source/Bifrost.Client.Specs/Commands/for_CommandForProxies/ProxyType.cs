
using Bifrost.Commands;
using Castle.DynamicProxy;
namespace Bifrost.Client.Specs.Commands.for_CommandForProxies
{
    public class ProxyType<T> : ICommandFor<T>, IHoldCommandInstance where T: ICommand
    {
        public ProxyType(IInterceptor[] interceptors)
        {

        }

        public T Instance { get; set; }
        public ICommand CommandInstance { get; set; }

        public bool CanExecute(object parameter)
        {
            throw new System.NotImplementedException();
        }

        public event System.EventHandler CanExecuteChanged = (s, e) => { };

        public void Execute(object parameter)
        {
            throw new System.NotImplementedException();
        }

    }
}
