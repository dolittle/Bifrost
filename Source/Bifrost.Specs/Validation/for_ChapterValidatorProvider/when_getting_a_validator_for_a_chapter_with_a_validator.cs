using System;
using Bifrost.Fakes.Sagas;
using Bifrost.Validation;
using Machine.Specifications;

namespace Bifrost.Specs.Validation.for_ChapterValidatorProvider
{
    [Subject(typeof (ChapterValidatorProvider))]
    public class when_getting_a_validator_for_a_chapter_with_a_validator : given.a_chapter_validator_provider
    {
        static IChapterValidator chapter_validator;

        Establish context = () => service_locator_mock.Setup(sl => sl.GetInstance(typeof(TransitionalChapterValidator))).Returns(() => new TransitionalChapterValidator());

        Because of = () => chapter_validator = chapter_validator_provider.GetValidatorFor(new TransitionalChapter());

        It should_return_the_correct_validator = () => chapter_validator.ShouldBeOfType(typeof(TransitionalChapterValidator));
    }
}