using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Bifrost.Commands;
using System.Linq;

namespace Bifrost.Web.Mvc.Commands
{
    public static class ModelStateDictionaryExtensions
    {
        public static void FromCommandResult(this ModelStateDictionary modelStateDictionary, CommandResult commandResult, string exceptionErrorName)
        {
            if (commandResult.Invalid)
            {
                modelStateDictionary.AddToModelErrors(commandResult.ValidationResults);
            }
            else
            {
                modelStateDictionary.AddModelError(exceptionErrorName, commandResult.Exception.Message);
            }
        }

        public static void AddToModelErrors(this ModelStateDictionary modelStateDictionary, IEnumerable<ValidationResult> validationResults, string prefix)
        {
            foreach (var validationResult in validationResults)
            {
                foreach (var memberName in validationResult.MemberNames)
                {
                    var key = string.IsNullOrWhiteSpace(prefix) ? memberName : string.Concat(prefix, ".", memberName);
                    modelStateDictionary.AddModelError(key, validationResult.ErrorMessage);
                }
            }
        }

        public static void AddToModelErrors(this ModelStateDictionary modelStateDictionary, IEnumerable<ValidationResult> validationResults)
        {
            AddToModelErrors(modelStateDictionary, validationResults, string.Empty);
        }

        public static CommandResult ToCommandResult(this ModelStateDictionary modelStateDictionary)
        {
            var validationResults = new List<ValidationResult>();
            foreach (var ms in modelStateDictionary)
            {
                var key = ms.Key;
                if (ms.Value.Errors.Count > 0)
                {
                    validationResults.AddRange(ms.Value.Errors.Select(error => new ValidationResult(error.ErrorMessage, new[] { key })));
                }
            }

            return new CommandResult() { ValidationResults = validationResults };
        }
    }
}
