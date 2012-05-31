using System.Reflection;

namespace Bifrost.Specs.Resources.for_ResourceInterceptor
{
    public class ClassWithString
    {
        public string SomeString { get; set; }

        public static PropertyInfo SomeStringProperty { get { return typeof(ClassWithString).GetProperty("SomeString"); } }
    }
}
