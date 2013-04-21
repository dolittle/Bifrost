using Bifrost.Tasks;
using System.Collections.Generic;

namespace Bifrost.Specs.Tasks.for_TaskManager
{
    public class OurTask : Task
    {
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
        public void FirstOperation(Task task, int operationIndex)
        {
            FirstOperationCalled = true;
        }


        public bool SecondOperationCalled = false;
        public void SecondOperation(Task task, int operationIndex)
        {
            SecondOperationCalled = true;
        }


        public bool BeginCalled = false;
        public override void Begin()
        {
            BeginCalled = true;
            base.Begin();
        }

        public bool EndCalled = false;
        public override void End()
        {
            EndCalled = true;
            base.End();
        }
    }
}
