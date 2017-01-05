/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bifrost.Validation;

namespace Bifrost.Commands
{
    /// <summary>
    /// Represents the result from the <see cref="ICommandCoordinator">CommandCoordinator</see>
    /// </summary>
    public class CommandResult
    {
        /// <summary>
        /// Initializes an instance of <see cref="CommandResult"/>
        /// </summary>
        public CommandResult()
        {
            ValidationResults = new ValidationResult[0];
            CommandValidationMessages = new string[0];
            SecurityMessages = new string[0];
        }


        /// <summary>
        /// Gets or sets the name of command that this result is related to
        /// </summary>
        public string CommandName { get; set; }

        /// <summary>
        /// Gets or sets the Id of the command that this result is related to
        /// </summary>
        public Guid CommandId { get; set; }

        /// <summary>
        /// Gets or sets the ValidationResults generated during handling of a command
        /// </summary>
        public IEnumerable<ValidationResult> ValidationResults { get; set; }

        /// <summary>
        /// Gets the error messages that are related to full command during validation
        /// </summary>
        public IEnumerable<string> CommandValidationMessages { get; set; }

        /// <summary>
        /// Gets the messages that are related to broken security rules
        /// </summary>
        public IEnumerable<string> SecurityMessages { get; set; }

        /// <summary>
        /// Gets any validation errors (for properties or for the full command) as a simple string enumerbale.
        /// To relate property validation errors to the relevant property, use the <see cref="ValidationResult">ValidationResults</see> property.
        /// </summary>
        public IEnumerable<string> AllValidationMessages
        {
            get { return CommandValidationMessages.Union(ValidationResults.Select(vr => vr.ErrorMessage)); }
        }

        /// <summary>
        /// Gets or sets the exception, if any, that occured during a handle
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// Gets or sets the exception message, if any
        /// </summary>
        public string ExceptionMessage { get; set; }

        /// <summary>
        /// Gets the success state of the result
        ///
        /// If there are invalid validationresult or command validattion messages, this is false.
        /// If an exception occured, this is false.
        /// Otherwise, its true
        /// </summary>
        public bool Success
        {
            get { return null == Exception && PassedSecurity && !Invalid; }
        }

        /// <summary>
        /// Gets the validation state of the result
        ///
        /// If there are any validationresults or command validation messages this returns false, true if not
        /// </summary>
        public bool Invalid
        {
            get { return (ValidationResults != null && ValidationResults.Any()) || (CommandValidationMessages != null && CommandValidationMessages.Any()); }
        }

        /// <summary>
        /// Gets or sets wether or not command passed security
        /// </summary>
        public bool PassedSecurity
        {
            get { return SecurityMessages != null && !SecurityMessages.Any(); }
        }

        /// <summary>
        /// Merges another CommandResult instance into the current instance
        /// </summary>
        /// <param name="commandResultToMerge">The source <see cref="CommandResult"/> to merge into current instance</param>
        public void MergeWith(CommandResult commandResultToMerge)
        {
            if (Exception == null)
                Exception = commandResultToMerge.Exception;

            MergeValidationResults(commandResultToMerge);
            MergeCommandErrorMessages(commandResultToMerge);
        }


        /// <summary>
        /// Create a <see cref="CommandResult"/> for a given <see cref="ICommand"/> instance
        /// </summary>
        /// <param name="command"><see cref="ICommand"/> to create from</param>
        /// <returns>A <see cref="CommandResult"/> with <see cref="ICommand"/> details populated</returns>
        public static CommandResult ForCommand(ICommand command)
        {
            return new CommandResult
            {
                CommandId = command.Id,
                CommandName = command.GetType().Name
            };
        }

        void MergeValidationResults(CommandResult commandResultToMerge)
        {
            if (commandResultToMerge.ValidationResults == null)
                return;

            if (ValidationResults == null)
            {
                ValidationResults = commandResultToMerge.ValidationResults;
                return;
            }

            var validationResults = ValidationResults.ToList();
            validationResults.AddRange(commandResultToMerge.ValidationResults);
            ValidationResults = validationResults.ToArray();
        }

        void MergeCommandErrorMessages(CommandResult commandResultToMerge)
        {
            if (commandResultToMerge.CommandValidationMessages == null)
                return;

            if (CommandValidationMessages == null)
            {
                CommandValidationMessages = commandResultToMerge.CommandValidationMessages;
                return;
            }

            var commandErrorMessages = CommandValidationMessages.ToList();
            commandErrorMessages.AddRange(commandResultToMerge.CommandValidationMessages);
            CommandValidationMessages = commandErrorMessages.ToArray();
        }

        /// <summary>
        /// Returns a string that represents the state of the <see cref="CommandResult"/>
        /// </summary>
        /// <returns><see cref="String"/> with full detail from the <see cref="CommandResult"/></returns>
        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("Success : {0}", Success);
            stringBuilder.AppendFormat(", Invalid : {0}", Invalid);

            if (Exception != null)
                stringBuilder.AppendFormat(", Exception : {0}", Exception.Message);

            if (ExceptionMessage != null)
                stringBuilder.AppendFormat(", ExceptionMesssage : {0}", ExceptionMessage);

            return stringBuilder.ToString();
        }
    }
}