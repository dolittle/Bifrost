using Bifrost.Commands;
using System;

namespace Bifrost.Silverlight.Specs.Commands.for_CommandBuilder
{
    public class KnownCommand : ICommand
    {
        public KnownCommand()
        {
            Something = "HEllo world";
        }

        public string Something { get; set; }

        public string Name { get; set; } 
        public dynamic Parameters { get; set; }
        public bool IsBusy { get; set; }
        public bool IsProcessing { get; set; }
       
        public Guid Id { get; set; }

        public bool CanExecute(object parameter)
        {
            throw new System.NotImplementedException();
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            throw new System.NotImplementedException();
        }
    }
}
