/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using FluentValidation.Results;
using Machine.Specifications;

namespace Bifrost.MSpec.Extensions
{
    public static class FluentValidationExtensions
    {
        public static void ShouldBeValid(this ValidationResult validationResult)
        {
            validationResult.IsValid.ShouldBeTrue();
        }

        public static void ShouldBeInvalid(this ValidationResult validationResult)
        {
            validationResult.IsValid.ShouldBeFalse();
        }
    }
}
