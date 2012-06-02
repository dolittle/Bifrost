using Bifrost.Fakes.Sagas;
using Bifrost.Validation;
using Machine.Specifications;

namespace Bifrost.Specs.Validation.for_ChapterValidatorProvider
{
    [Subject(typeof (ChapterValidatorProvider))]
    public class when_getting_a_validator_for_a_chapter_transition_with_no_validator : given.a_chapter_validator_provider
    {
        static IChapterValidator chapter_validator;

        Because of = () => chapter_validator = chapter_validator_provider.GetValidatorForTransitionTo<TransitionalChapter>(new SimpleChapter());

        It should_return_the_correct_validator = () => chapter_validator.ShouldBeOfType(typeof(NullChapterValidator));
    }
}