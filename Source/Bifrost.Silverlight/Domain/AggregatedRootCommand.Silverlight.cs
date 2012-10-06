using System;
using System.Dynamic;

namespace Bifrost.Domain
{
    public partial class AggregatedRootCommand<T>
    {
        dynamic _parameters;

        public string Name { get; set; }
        public dynamic Parameters
        {
            get
            {
                if (_parameters == null)
                    _parameters = new ExpandoObject();
                return _parameters;
            }
        }

        public bool IsBusy { get; set; }

        public bool CanExecute(object parameter)
        {
            return true;
        }
        public event EventHandler CanExecuteChanged = (s, e) => { };

        public void Execute(object parameter)
        {
        }

    }
}
