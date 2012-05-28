using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Bifrost.Validation;
using Machine.Specifications;

namespace Bifrost.Specs.Validation.for_ChapterTransitionValidator
{
    [Subject(typeof(ChapterTranstionValidator<,>))]
    public class when_validating_an_invalid_chapter_tansition : given.a_chapter_transition_validator
    {
        static IEnumerable<ValidationResult> results;

        Establish context = () =>
                                {
                                    transitional_chapter.Something = "";
                                    transitional_chapter.AnInteger = 0;
                                };

        Because of = () => results = transition_validator.ValidateChapter(transitional_chapter);

        It should_have_invalid_properties = () => results.Count().ShouldEqual(2);
    }
}