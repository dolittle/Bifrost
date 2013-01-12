using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Bifrost.Validation;

namespace Bifrost.Testing.Fakes.Commands
{
    public class SimpleCommandBusinessValidator : CommandBusinessValidator<SimpleCommand>
    {
        public override IEnumerable<ValidationResult> ValidateFor(SimpleCommand instance)
        {
            throw new NotImplementedException();
        }
    }
}