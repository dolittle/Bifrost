using Bifrost.Commands;
using Bifrost.Specs.Concepts.given;
using Bifrost.Validation;
using FluentValidation;

namespace Bifrost.Specs.Validation
{
    public class commands
    {
        public class MySimpleCommand : Command
        {
            public concepts.StringConcept StringConcept { get; set; }
            public concepts.LongConcept LongConcept { get; set; }
        }

        public class StringConceptInputValidator : InputValidator<concepts.StringConcept>
        {
            public StringConceptInputValidator()
            {
                RuleFor(s => s.Value)
                    .NotEmpty();
            }
        }

        public class StringConceptBusinessValidator : BusinessValidator<concepts.StringConcept>
        {
            public StringConceptBusinessValidator()
            {
                RuleFor(s => s.Value)
                    .Equals("Blah");
            }
        }

        public class LongConceptInputValidator : InputValidator<concepts.LongConcept>
        {
            public LongConceptInputValidator()
            {
                RuleFor(s => s.Value)
                    .NotEmpty();
            }
        }

        public class LongConceptBusinessValidator : BusinessValidator<concepts.LongConcept>
        {
            public LongConceptBusinessValidator()
            {
                RuleFor(s => s.Value)
                    .GreaterThanOrEqualTo(100);
            }
        }
    }
}