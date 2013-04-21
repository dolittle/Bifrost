using Bifrost.Execution;
using Bifrost.Testing.Fakes.Sagas;
using Bifrost.Validation;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Validation.for_ChapterValidatorProvider.given
{
    public class a_chapter_validator_provider
    {
        protected static IChapterValidatorProvider chapter_validator_provider;

        protected static Mock<IContainer> container_mock;
        protected static Mock<ITypeDiscoverer> type_discoverer_mock;

        Establish context = () =>
                                {
                                    container_mock = new Mock<IContainer>();
                                    type_discoverer_mock = new Mock<ITypeDiscoverer>();

                                    type_discoverer_mock.Setup(td => td.FindMultiple(typeof(IChapterValidator)))
                                        .Returns(new[]
                                                {
                                                    typeof(TransitionalChapterValidator),
                                                    typeof(NonTransitionalChapterValidator),
                                                    typeof(NullChapterValidator)
                                                }
                                        );

                                    chapter_validator_provider = new ChapterValidatorProvider(type_discoverer_mock.Object, container_mock.Object);
                                };
    }
}