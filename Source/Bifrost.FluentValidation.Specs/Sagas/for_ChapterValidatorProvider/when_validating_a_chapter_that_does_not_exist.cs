using Bifrost.FluentValidation.Sagas;
using Bifrost.Sagas;
using Bifrost.Validation;
using Machine.Specifications;

namespace Bifrost.FluentValidation.Specs.Sagas.for_ChapterValidatorProvider
{
    [Subject(typeof (ChapterValidatorProvider))]
    public class when_validating_a_chapter_that_does_not_exist : given.a_chapter_validator_provider
    {
        static ICanValidate validator;

        Because of = () => validator = chapter_validator_provider.GetValidatorFor(null as IChapter);

        It should_return_a_null_validator = () => validator.ShouldBeOfExactType(typeof(NullChapterValidator));
    }
}