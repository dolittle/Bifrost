using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Bifrost.Fakes.Sagas;
using Bifrost.Sagas;
using Bifrost.Validation;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Validation.for_ChapterValidationService
{
    [Subject(typeof(ChapterValidationService))]
    public class when_validating_a_chapter_transition_that_passes
    {
        static IEnumerable<ValidationResult> validation_results;
        static ChapterValidationService chapter_validation_service;
        static Mock<IChapterValidatorProvider> chapter_validator_provider_mock;
        static Mock<IChapterTransitionValidator> chapter_transition_validator_mock;
        static Mock<IChapter> chapter_mock;

        Establish context = () =>
                                {
                                    chapter_mock = new Mock<IChapter>();


                                    chapter_transition_validator_mock = new Mock<IChapterTransitionValidator>();
                                    chapter_transition_validator_mock.Setup(ctv => ctv.ValidateChapter(chapter_mock.Object)).Returns(new ValidationResult[]{});

                                    chapter_validator_provider_mock = new Mock<IChapterValidatorProvider>();
                                    chapter_validator_provider_mock.Setup(cvm => cvm.GetValidatorForTransitionTo<SimpleChapter>(chapter_mock.Object)).Returns(chapter_transition_validator_mock.Object);

                                    chapter_validation_service = new ChapterValidationService(chapter_validator_provider_mock.Object);
                                };



        Because of = () => validation_results = chapter_validation_service.ValidateForTransistionTo<SimpleChapter>(chapter_mock.Object);

        It should_have_no_failed_validation_results = () => validation_results.ShouldBeEmpty();
        It should_have_validated_the_chapter = () => chapter_validator_provider_mock.VerifyAll();

    }

}
