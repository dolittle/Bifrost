using System;
using System.Dynamic;

namespace Bifrost.Commands
{
#pragma warning disable 1591 // Xml Comments
    public partial class DynamicCommand
    {
        dynamic _parameters;

        public bool IsBusy { get; set; }
        public bool IsProcessing { get; set; }
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

        public bool CanExecute(object parameter)
        {
            return true;
        }
        public event EventHandler CanExecuteChanged = (s, e) => { };

        public void Execute(object parameter)
        {
        }
    }
#pragma warning restore 1591 // Xml Comments
}
