using Bifrost.Commands;

namespace Bifrost.Silverlight.Specs.Commands.for_CommandFactory
{
    public class ViewModelWithStringProperty
    {
        public ICommand TestCommand { get; set; }
        public ICommand SecondCommand { get; set; }

        public string StringCommand { get; set; }
    }
}
