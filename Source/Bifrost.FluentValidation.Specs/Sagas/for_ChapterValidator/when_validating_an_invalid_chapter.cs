using System.Collections.Generic;
using System.Linq;
using Bifrost.FluentValidation.Sagas;
using Bifrost.Validation;
using Machine.Specifications;

namespace Bifrost.FluentValidation.Specs.Sagas.for_ChapterValidator
{
    [Subject(typeof(ChapterValidator<>))]
    public class when_validating_an_invalid_chapter : given.a_chapter_validator
    {
        static IEnumerable<ValidationResult> results;

        Establish context = () =>
                                {
                                    transitional_chapter.Something = "";
                                    transitional_chapter.AnInteger = 0;
                                };

        Because of = () => results = transitional_chapter_validator.ValidateFor(transitional_chapter);

        It should_have_invalid_properties = () => ShouldExtensionMethods.ShouldEqual(results.Count(), 2);
    }
}