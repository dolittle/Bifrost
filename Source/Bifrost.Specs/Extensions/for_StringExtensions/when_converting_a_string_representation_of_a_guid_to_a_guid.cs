using System;
using Bifrost.Extensions;
using Machine.Specifications;

namespace Bifrost.Specs.Extensions.for_StringExtensions
{
    [Subject(typeof(StringExtensions))]
    public class when_converting_a_string_representation_of_a_guid_to_a_guid
    {
        static string guid_as_a_string;
        static Guid result;

        Establish context = () =>
            {
                guid_as_a_string = Guid.NewGuid().ToString();
            };

        Because of = () => result = (Guid)guid_as_a_string.ParseTo(typeof(Guid));

        It should_create_a_guid = () => result.ShouldBeOfExactType<Guid>();
        It should_have_the_correct_value = () => result.ToString().ShouldEqual(guid_as_a_string);
    }
}