using System.ComponentModel;

namespace Bifrost.Client.Specs.Interaction.for_CommandForMethod
{
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (s, e) => { };

        bool _canExecute;
        public bool CanExecute
        {
            get { return _canExecute;  }
            set
            {
                _canExecute = value;
                PropertyChanged(this, new PropertyChangedEventArgs("CanExecute"));
            }
        }

        public void Method()
        {

        }
    }
}
