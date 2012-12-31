using Bifrost.Tasks;
using System.Threading;

namespace Bifrost.Mimir.Views.EventStores
{
    public class ResetAllEventsForAllSubscriptionsTask : Task
    {
        public override TaskOperation[] Operations
        {
            get { return new TaskOperation[] { Perform }; }
        }

        public int Counter { get; set; }

        void Perform(Task task, int operationIndex)
        {
            while (Counter < 3)
            {
                Counter++;
                Progress();
                Thread.Sleep(1000);
            }
        }
    }
}
