using Bifrost.Testing.Fakes.Sagas;
using Machine.Specifications;

namespace Bifrost.Specs.Validation.for_ChapterValidator.given
{
    public class a_chapter_validator
    {
        protected static TransitionalChapterValidator transitional_chapter_validator;
        protected static TransitionalChapter transitional_chapter;

        Establish context = () =>
        {
            transitional_chapter_validator = new TransitionalChapterValidator();
            transitional_chapter = new TransitionalChapter();
        };
    }
}