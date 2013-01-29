using System.Collections.Generic;
using Bifrost.Security;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Security.for_Securable
{
    [Subject(typeof(SecurityActor))]
    public class when_adding_a_security_actor
    {
        static Securable security_target;
        static Mock<ISecurityActor> security_actor_mock;

        Establish context = () => 
        {
            security_target = new NamespaceSecurable("Bifrost.Security");
            security_actor_mock = new Mock<ISecurityActor>();
        };

        Because of = () => security_target.AddActor(security_actor_mock.Object);

        It should_have_it_available_in_the_collection = () => security_target.Actors.ShouldContain(security_actor_mock.Object);
    }

    //[Subject(typeof(SecurityActor))]
    //public class when_adding_a_security_actor
    //{
    //    static Securable security_target;
    //    static Mock<ISecurityActor> first_security_actor;
    //    static Mock<ISecurityActor> second_security_actor;
    //    static IEnumerable<string> description;

    //    Establish context = () =>
    //    {
    //        security_target = new NamespaceSecurable("Bifrost.Security");
    //        first_security_actor = new Mock<ISecurityActor>();
    //        second_security_actor = new Mock<ISecurityActor>();

    //        security_target.AddActor(first_security_actor.Object);
    //        security_target.AddActor(second_security_actor.Object);
    //    };

    //    Because of = () => description = security_target.Description;

    //    It should_have_it_available_in_the_collection = () => security_target.Actors.ShouldContain(security_actor_mock.Object);
    //}
}
