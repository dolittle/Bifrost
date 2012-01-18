using Bifrost.Validation;
using FluentValidation;

namespace Bifrost.Samples.Blog.Domain.Posts.Commands
{
    public class CreatePostValidator : CommandInputValidator<CreatePost>
    {
        public CreatePostValidator()
        {
            RuleFor(c => c.Title).NotEmpty().WithMessage(ValidationMessages.TitleRequired);
            RuleFor(c => c.Body).NotEmpty().WithMessage(ValidationMessages.BodyRequired);
        }

        public override FluentValidation.Results.ValidationResult Validate(CreatePost instance)
        {
            var result =  base.Validate(instance);
            return result;
        }

        public override FluentValidation.Results.ValidationResult Validate(ValidationContext<CreatePost> context)
        {
            var result = base.Validate(context);
            return result;
        }
    }
}