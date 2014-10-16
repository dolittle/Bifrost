using System;
using Bifrost.Testing.Fakes.Sagas;
using Bifrost.Validation;
using Machine.Specifications;

namespace Bifrost.Specs.Validation.for_ChapterValidatorProvider
{
    [Subject(typeof (ChapterValidatorProvider))]
    public class when_getting_a_validator_for_a_chapter_with_a_validator : given.a_chapter_validator_provider
    {
        static ICanValidate chapter_validator;

        Establish context = () => container_mock.Setup(c => c.Get(typeof(TransitionalChapterValidator))).Returns(() => new TransitionalChapterValidator());

        Because of = () => chapter_validator = chapter_validator_provider.GetValidatorFor(new TransitionalChapter());

        It should_return_the_correct_validator = () => chapter_validator.ShouldBeOfType(typeof(TransitionalChapterValidator));
    }
}