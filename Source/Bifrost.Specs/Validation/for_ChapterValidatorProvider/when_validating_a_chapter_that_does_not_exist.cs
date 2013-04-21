using Bifrost.Sagas;
using Bifrost.Validation;
using Machine.Specifications;

namespace Bifrost.Specs.Validation.for_ChapterValidatorProvider
{
    [Subject(typeof (ChapterValidatorProvider))]
    public class when_validating_a_chapter_that_does_not_exist : given.a_chapter_validator_provider
    {
        static ICanValidate validator;

        Because of = () => validator = chapter_validator_provider.GetValidatorFor(null as IChapter);

        It should_return_a_null_validator = () => validator.ShouldBeOfType(typeof(NullChapterValidator));
    }
}