using Bifrost.Utils;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Utils.for_StringMapper
{
    public class when_asking_if_has_mapping_for_and_no_one_can_resolve
    {
        const string input = "something";
        static StringMapper mapper = new StringMapper();
        static Mock<IStringMapping> first_mapping_mock;
        static Mock<IStringMapping> second_mapping_mock;
        static bool result;

        Establish context = () =>
        {
            first_mapping_mock = new Mock<IStringMapping>();
            first_mapping_mock.Setup(f => f.Matches(input)).Returns(false);
            second_mapping_mock = new Mock<IStringMapping>();
            second_mapping_mock.Setup(f => f.Matches(input)).Returns(false);
            mapper.AddMapping(first_mapping_mock.Object);
            mapper.AddMapping(second_mapping_mock.Object);
        };

        Because of = () => result = mapper.HasMappingFor(input);

        It should_return_false = () => result.ShouldBeFalse();

    }
}
