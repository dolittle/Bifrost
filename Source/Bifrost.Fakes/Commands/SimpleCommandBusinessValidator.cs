using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Bifrost.Commands;
using Bifrost.Validation;

namespace Bifrost.Fakes.Commands
{
    public class SimpleCommandBusinessValidator : CommandBusinessValidator<SimpleCommand>
    {
        public override IEnumerable<ValidationResult> Validate(SimpleCommand instance)
        {
            throw new NotImplementedException();
        }
    }
}