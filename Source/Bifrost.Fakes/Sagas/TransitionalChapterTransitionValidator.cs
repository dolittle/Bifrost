using Bifrost.Validation;
using FluentValidation;

namespace Bifrost.Fakes.Sagas
{
    public class TransitionalChapterTransitionValidator : ChapterTranstionValidator<TransitionalChapter, SimpleChapter>
    {
        public TransitionalChapterTransitionValidator()
        {
            RuleFor(c => c.Something).NotEmpty();
            RuleFor(c => c.AnInteger).GreaterThanOrEqualTo(1);
        }

    }
}