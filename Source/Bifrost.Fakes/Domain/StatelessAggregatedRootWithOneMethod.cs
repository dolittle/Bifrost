using System;
using Bifrost.Domain;

namespace Bifrost.Fakes.Domain
{
    public class StatelessAggregatedRootWithOneMethod : AggregatedRoot
    {
        public StatelessAggregatedRootWithOneMethod(Guid id) : base(id)
        {
        }

        public static void ResetState()
        {
            DoSomethingCalled = false;
        }

        public static bool DoSomethingCalled = false;
        public void DoSomething(string input)
        {
            DoSomethingCalled = true;
        }
    }
}