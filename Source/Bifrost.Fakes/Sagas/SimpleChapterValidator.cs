using Bifrost.Validation;
using FluentValidation;

namespace Bifrost.Fakes.Sagas
{
    public class TransitionalChapterValidator : ChapterValidator<TransitionalChapter>
    {
        public TransitionalChapterValidator()
        {
            RuleFor(c => c.Something).NotEmpty();
            RuleFor(c => c.AnInteger).GreaterThanOrEqualTo(1);
        }
    }
}