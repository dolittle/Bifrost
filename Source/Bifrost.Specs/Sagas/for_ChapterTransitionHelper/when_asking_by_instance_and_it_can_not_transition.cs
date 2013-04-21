using Bifrost.Testing.Fakes.Sagas;
using Bifrost.Sagas;
using Machine.Specifications;

namespace Bifrost.Specs.Sagas.for_ChapterTransitionHelper
{
    public class when_asking_by_instance_and_it_can_not_transition 
    {
        static TransitionalChapter _transitionalChapter;
        static NonTransitionalChapter _nonTransitionalChapter;
        static bool can_transition;

        Establish context = () =>
                                {
                                    _transitionalChapter = new TransitionalChapter();
                                    _nonTransitionalChapter = new NonTransitionalChapter();
                                };

		Because of = () => can_transition = ChapterTransitionHelper.CanTransition(_nonTransitionalChapter, _transitionalChapter);

        It should_be_able_to_transition = () => can_transition.ShouldBeFalse();
    }
}