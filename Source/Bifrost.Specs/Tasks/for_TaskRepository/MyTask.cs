using Bifrost.Tasks;

namespace Bifrost.Specs.Tasks.for_TaskRepository
{
    public class MyTask : Task
    {
        public string AString { get; set; }
        public int AnInteger { get; set; }

        public override TaskOperation[] Operations
        {
            get { throw new System.NotImplementedException(); }
        }
    }
}
