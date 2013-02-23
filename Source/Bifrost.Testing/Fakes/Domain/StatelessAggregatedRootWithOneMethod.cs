using System;
using Bifrost.Domain;

namespace Bifrost.Testing.Fakes.Domain
{
    public class StatelessAggregatedRootWithOneMethod : AggregateRoot
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