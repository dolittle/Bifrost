using Bifrost.Tasks;
using System;

namespace Bifrost.Specs.Tasks.for_TaskScheduler
{
    public class TaskWithTwoOperations : Task
    {
        bool _runAsynchronously;
        public TaskWithTwoOperations(bool runAsynchronously)
        {
            _runAsynchronously = runAsynchronously;
        }

        public override TaskOperation[] Operations
        {
            get 
            { 
                return new TaskOperation[] {
                    FirstOperation,
                    SecondOperation
                };
            }
        }


        public bool FirstOperationCalled = false;
        public int FirstOperationIndex = -1;
        public Action FirstOperationCallback;
        void FirstOperation(Task task, int operationIndex)
        {
            FirstOperationCalled = true;
            FirstOperationIndex = operationIndex;
            if (FirstOperationCallback != null)
                FirstOperationCallback();
        }

        public bool SecondOperationCalled = false;
        public int SecondOperationIndex = -1;
        public Action SecondOperationCallback;
        void SecondOperation(Task task, int operationIndex)
        {
            SecondOperationCalled = true;
            SecondOperationIndex = operationIndex;
            if (SecondOperationCallback != null)
                SecondOperationCallback();
        }


        public override bool CanRunOperationsAsynchronously { get { return _runAsynchronously; } }
    }
}
