using System.Collections.Generic;
using System.ComponentModel;
using Bifrost.Values;

namespace Bifrost.Tasks
{
    public class Tasks : ITasks, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (s, e) => { };
        public IEnumerable<TaskResult> Results { get; private set; }

        bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                PropertyChanged.Notify(() => IsBusy);
            }
        }

        public void Execute(ITask task)
        {
        }
    }
}
