using System.Linq;
using Bifrost.Security;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Security.for_SecurityDescriptorBuilder
{
    public class when_adding_a_security_action_builder
    {
        static SecurityDescriptorBuilder   descriptor_builder;
        static Mock<ISecurityActionBuilder> action_builder_mock;

        Establish context = () => 
        {
            descriptor_builder = new SecurityDescriptorBuilder();
            action_builder_mock = new Mock<ISecurityActionBuilder>();
        };

        Because of = () => descriptor_builder.AddActionBuilder(action_builder_mock.Object);

        It should_have_one_action_builder = () => descriptor_builder.Actions.Count().ShouldEqual(1);
        It should_have_the_added_action_builder = () => descriptor_builder.Actions.First().ShouldEqual(action_builder_mock.Object);
        
    }
}
