using System;
using Bifrost.Sagas;
using Bifrost.Validation;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Validation.for_ChapterValidatorProvider
{
    [Subject(typeof (ChapterValidatorProvider))]
    public class when_getting_a_validator_for_chapter_with_no_validator : given.a_chapter_validator_provider
    {
        static ICanValidate chapter_validator;
        static Mock<IChapter> chapter_mock;

        Establish context = () => chapter_mock = new Mock<IChapter>();

        Because of = () => chapter_validator = chapter_validator_provider.GetValidatorFor(chapter_mock.Object);

        It should_return_a_null_chapter_validator = () => chapter_validator.ShouldBeOfType(typeof(NullChapterValidator));
    }
}