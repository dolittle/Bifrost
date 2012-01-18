using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Bifrost.Commands;
using Bifrost.Validation;

namespace Bifrost.Fakes.Commands
{
    public class AnotherSimpleCommandBusinessValidator : CommandBusinessValidator<AnotherSimpleCommand>
    {
        public override IEnumerable<ValidationResult> Validate(AnotherSimpleCommand instance)
        {
            throw new NotImplementedException();
        }
    }
}