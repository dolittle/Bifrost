/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Commands
{
    /// <summary>
    /// Defines the validators for validating a <see cref="ICommand"/>
    /// </summary>
    public interface ICommandValidators
    {
        /// <summary>
        /// Validate the command
        /// </summary>
        /// <param name="command">Instance to be validated</param>
        /// <returns>Validation results for a <see cref="ICommand">Command</see></returns>
        CommandValidationResult Validate(ICommand command);
    }
}
