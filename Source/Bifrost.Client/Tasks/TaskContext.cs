using System.ComponentModel;
using Bifrost.Values;

namespace Bifrost.Tasks
{
    public class TaskContext : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (s,e) => {};

        public TaskContext()
        {
            Result = new TaskResult();
            _progress = 0;
        }

        public TaskResult Result { get; private set; }

        double _progress;
        public double Progress
        {
            get { return _progress; }
            set
            {
                _progress = value;
                PropertyChanged.Notify(() => Progress);
            }
        }
    }
}
