using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Bifrost.Validation;
using Machine.Specifications;

namespace Bifrost.Specs.Validation.for_ChapterValidator
{
    [Subject(typeof (ChapterValidator<>))]
    public class when_validating_a_valid_chapter : given.a_chapter_validator
    {
        static IEnumerable<ValidationResult> results;

        Establish context = () =>
        {
            transitional_chapter.Something = "Something, something, something, Dark Side";
            transitional_chapter.AnInteger = 42;
        };

        Because of = () => results = transitional_chapter_validator.ValidateFor(transitional_chapter);

        It should_have_no_invalid_properties = () => results.ShouldBeEmpty();
    }
}