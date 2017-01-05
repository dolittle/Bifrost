/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Bifrost.Execution;
using Bifrost.Values;

namespace Bifrost.Tasks
{
    /// <summary>
    /// Represents an implementation of <see cref="ITasks"/>
    /// </summary>
    public class Tasks : ITasks, INotifyPropertyChanged
    {
        ObservableCollection<ITask> _all = new ObservableCollection<ITask>();
        ObservableCollection<TaskContext> _contexts = new ObservableCollection<TaskContext>();
        bool _isBusy;
        IDispatcher _dispatcher;

#pragma warning disable 1591 // Xml Comments

        public Tasks(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }


        public event PropertyChangedEventHandler PropertyChanged = (s, e) => { };
       
        public IEnumerable<ITask> All { get { return _all; } }

        public IEnumerable<TaskContext> Contexts { get { return _contexts; } }

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                PropertyChanged.Notify(() => IsBusy);
            }
        }

        public TaskContext Execute(ITask task, object associatedData = null)
        {
            var context = new TaskContext(task, associatedData);

            _dispatcher.BeginInvoke(() =>
            {
                _all.Add(task);
                _contexts.Add(context);
                UpdateBusy();
            });


            task.Execute(context).ContinueWith((p, d) =>
            {
                _dispatcher.BeginInvoke(() =>
                {
                    _all.Remove(task);
                    _contexts.Remove(context);
                    UpdateBusy();
                });
            }).Failed((p, d) =>
            {

            });

            return context;
        }


        void UpdateBusy()
        {
            IsBusy = _all.Count > 0;
        }
#pragma warning restore 1591 // Xml Comments
    }
}
