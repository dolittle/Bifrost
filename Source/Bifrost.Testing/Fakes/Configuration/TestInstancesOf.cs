using System.Collections;
using System.Collections.Generic;
using Bifrost.Execution;

namespace Bifrost.Testing.Fakes.Configuration
{
    public class TestInstancesOf<T> : IInstancesOf<T>, IOrderedInstancesOf<T> where T : class
    {
        readonly IEnumerable<T> _instances;

        public TestInstancesOf(params T[] instances)
        {
            _instances = instances;
        }

        public TestInstancesOf(IEnumerable<T> instances)
        {
            _instances = instances;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _instances.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
