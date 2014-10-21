using Bifrost.FluentValidation.Sagas;
using Bifrost.Testing.Fakes.Sagas;
using global::FluentValidation;

namespace Bifrost.FluentValidation.Specs.Sagas
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