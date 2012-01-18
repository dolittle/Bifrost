using Bifrost.Execution;
using Bifrost.Validation;
using Machine.Specifications;
using Microsoft.Practices.ServiceLocation;
using Moq;

namespace Bifrost.Specs.Validation.for_ChapterValidatorProvider.given
{
    public class a_chapter_validator_provider
    {
        protected static IChapterValidatorProvider chapter_validator_provider;

        protected static Mock<IServiceLocator> service_locator_mock;
        protected static Mock<ITypeDiscoverer> type_discoverer_mock;

        Establish context = () =>
                                {
                                    service_locator_mock = new Mock<IServiceLocator>();
                                    type_discoverer_mock = new Mock<ITypeDiscoverer>();

                                    type_discoverer_mock.Setup(td => td.FindMultiple(typeof(IChapterValidator)))
                                        .Returns(new[]
                                                {
                                                    typeof(Fakes.Sagas.TransitionalChapterValidator),
                                                    typeof(Fakes.Sagas.NonTransitionalChapterValidator),
                                                    typeof(NullChapterValidator)
                                                }
                                        );

                                    chapter_validator_provider = new ChapterValidatorProvider(type_discoverer_mock.Object, service_locator_mock.Object);
                                };
    }
}