using Machine.Specifications;

namespace Bifrost.Specs.Resources.for_ResourceInterceptor
{
    public class when_intercepting : given.a_resource_interceptor
    {
        const string expected = "Something";
        static FakeInvocation invocation;
        static ClassWithString resources;
        static string resource_string;

        Establish context = () =>
        {
            resources = new ClassWithString();
            invocation = new FakeInvocation();
            invocation.Method = ClassWithString.SomeStringProperty.GetGetMethod();
            invocation.ReturnValueToSetOnProceed = expected;
            resource_resolver_mock.Setup(r=>r.Resolve(Moq.It.IsAny<string>())).Callback((string s) => resource_string = s);
        };

        Because of = () => interceptor.Intercept(invocation);

        It should_combine_type_and_propertyname_as_resource_name = () => resource_string.ShouldEqual(typeof(ClassWithString).Name + "." + ClassWithString.SomeStringProperty.Name);
    }
}
