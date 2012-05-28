using Bifrost.Fakes.Sagas;
using Machine.Specifications;

namespace Bifrost.Specs.Validation.for_ChapterTransitionValidator.given
{
    public class a_chapter_transition_validator
    {
        protected static TransitionalChapterTransitionValidator transition_validator;
        protected static TransitionalChapter transitional_chapter;

        Establish context = () =>
        {
            transition_validator = new TransitionalChapterTransitionValidator();
            transitional_chapter = new TransitionalChapter();
        };
    }
}