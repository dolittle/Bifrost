using System.Collections.Generic;
using Bifrost.Testing.Fakes.Configuration;

namespace Bifrost.Testing
{
    public static class TestExtensions
    {
        public static TestInstancesOf<T> NoInstancesOf<T>() where T : class
        {
            return new TestInstancesOf<T>();
        }

        public static TestInstancesOf<T> SingleInstanceOf<T>(this T instance) where T : class
        {
            return new TestInstancesOf<T>(instance);
        }

        public static TestInstancesOf<T> AsInstancesOf<T>(this IEnumerable<T> instances) where T : class
        {
            return new TestInstancesOf<T>(instances);
        }
    }
}
