using Bifrost.Fakes.Sagas;
using Bifrost.Validation;
using Machine.Specifications;

namespace Bifrost.Specs.Validation.for_ChapterValidatorProvider
{
    [Subject(typeof (ChapterValidatorProvider))]
    public class when_getting_a_validator_for_a_chapter_transition_with_a_transition_validator : given.a_chapter_validator_provider
    {
        static IChapterValidator chapter_validator;

        Establish context = () => service_locator_mock.Setup(sl => sl.GetInstance(typeof(TransitionalChapterTransitionValidator))).Returns(() => new TransitionalChapterTransitionValidator());

        Because of = () => chapter_validator = chapter_validator_provider.GetValidatorForTransitionTo<SimpleChapter>(new TransitionalChapter());

        It should_return_the_correct_validator = () => chapter_validator.ShouldBeOfType(typeof(TransitionalChapterTransitionValidator));
    }
}