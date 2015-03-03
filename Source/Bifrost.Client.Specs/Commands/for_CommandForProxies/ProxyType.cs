
using Bifrost.Commands;
using Castle.DynamicProxy;
namespace Bifrost.Client.Specs.Commands.for_CommandForProxies
{
    public class ProxyType : ICommandFor<MyCommand>, IHoldCommandInstance
    {
        public ProxyType(IInterceptor[] interceptors)
        {

        }

        public MyCommand Instance { get; set; }
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
