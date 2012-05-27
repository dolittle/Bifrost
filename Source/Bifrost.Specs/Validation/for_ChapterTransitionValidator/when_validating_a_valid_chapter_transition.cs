using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Bifrost.Validation;
using Machine.Specifications;

namespace Bifrost.Specs.Validation.for_ChapterTransitionValidator
{
    [Subject(typeof(ChapterTranstionValidator<,>))]
    public class when_validating_a_valid_chapter_transition : given.a_chapter_transition_validator
    {
        static IEnumerable<ValidationResult> results;

        Establish context = () =>
                                {
                                    transitional_chapter.Something = "Something, something, something, Dark Side";
                                    transitional_chapter.AnInteger = 42;
                                };

        Because of = () => results = transition_validator.ValidateChapter(transitional_chapter);

        It should_have_no_invalid_properties = () => results.ShouldBeEmpty();
    }
}