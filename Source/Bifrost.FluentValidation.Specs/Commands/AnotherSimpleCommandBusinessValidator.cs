using System;
using System.Collections.Generic;
using Bifrost.FluentValidation.Commands;
using Bifrost.Validation;

namespace Bifrost.Testing.Fakes.Commands
{
    public class AnotherSimpleCommandBusinessValidator : CommandBusinessValidator<AnotherSimpleCommand>
    {
        public override IEnumerable<ValidationResult> ValidateFor(AnotherSimpleCommand instance)
        {
            throw new NotImplementedException();
        }
    }
}