using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Bifrost.FluentValidation.Commands;
using Bifrost.Testing.Fakes.Commands;

namespace Bifrost.FluentValidation.Specs.Commands
{
    public class SimpleCommandBusinessValidator : CommandBusinessValidator<SimpleCommand>
    {
        public override IEnumerable<ValidationResult> ValidateFor(SimpleCommand instance)
        {
            throw new NotImplementedException();
        }
    }
}