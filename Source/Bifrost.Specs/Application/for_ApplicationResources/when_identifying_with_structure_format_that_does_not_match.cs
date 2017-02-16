using System;
using Bifrost.Application;
using Bifrost.Strings;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Application.for_ApplicationResources
{
    public class when_identifying_with_structure_format_that_does_not_match : given.application_resources_with_one_structure_format
    {
        static Exception exception;


        Establish context = () =>
        {
            var matches = new Mock<ISegmentMatches>();
            matches.SetupGet(m => m.HasMatches).Returns(false);
            string_format.Setup(s => s.Match(typeof(string).Namespace)).Returns(matches.Object);
        };

        Because of = () => exception = Catch.Exception(() => resources.Identify("something"));

        It should_throw_unable_to_identify_application_resource = () => exception.ShouldBeOfExactType<UnableToIdentifyApplicationResource>();
    }
}
