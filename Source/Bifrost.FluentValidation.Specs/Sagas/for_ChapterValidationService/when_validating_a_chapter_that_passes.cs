using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Bifrost.Sagas;
using Bifrost.Validation;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Validation.for_ChapterValidationService
{
    [Subject(typeof (ChapterValidationService))]
    public class when_validating_a_chapter_that_passes : given.a_chapter_validation_service
    {
        static IEnumerable<ValidationResult> validation_results;
        static Mock<IChapter> chapter_mock;
        static Mock<ICanValidate> chapter_validator_mock;

        Establish context = () =>
        {
            chapter_mock = new Mock<IChapter>();
            chapter_validator_mock = new Mock<ICanValidate>();

            chapter_validator_mock.Setup(v => v.ValidateFor(chapter_mock.Object)).Returns(new List<ValidationResult>());

            chapter_validator_provider_mock.Setup(vp => vp.GetValidatorFor(chapter_mock.Object)).Returns(chapter_validator_mock.Object);
            };

        Because of = () => validation_results = chapter_validation_service.Validate(chapter_mock.Object);

        It should_have_no_failed_validation_results = () => validation_results.ShouldBeEmpty();
        It should_have_validated_the_chapter = () => chapter_validator_mock.VerifyAll();
    }
}