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
    public class when_validating_a_chapter_without_transition_validation_with_chapter_validation : given.a_chapter_validation_service
    {
        static IEnumerable<ValidationResult> validation_results;
        static Mock<IChapterValidator> chapter_validator_mock;
        static Mock<IChapterValidator> chapter_validator;
        static Mock<IChapter> chapter_mock;

        Establish context = () =>
                                {
                                    chapter_mock = new Mock<IChapter>();

                                    chapter_validator = new Mock<IChapterValidator>();
                                    chapter_validator.Setup(cv => cv.ValidateChapter(chapter_mock.Object)).Returns(new ValidationResult[]{});

                                    chapter_validator_provider_mock = new Mock<IChapterValidatorProvider>();
                                    chapter_validator_provider_mock.Setup(cvm => cvm.GetValidatorForTransitionTo<SimpleChapter>(chapter_mock.Object)).Returns(chapter_validator.Object);

                                    chapter_validation_service = new ChapterValidationService(chapter_validator_provider_mock.Object);

                                };



        Because of = () => validation_results = chapter_validation_service.ValidateTransistionTo<SimpleChapter>(chapter_mock.Object);

        It should_have_no_failed_validation_results = () => validation_results.ShouldBeEmpty();
        It should_have_validated_the_chapter = () => chapter_validator_provider_mock.VerifyAll();
    }
}