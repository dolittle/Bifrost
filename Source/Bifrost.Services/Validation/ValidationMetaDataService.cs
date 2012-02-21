using System;
using Bifrost.Commands;
using Bifrost.Validation;
using FluentValidation;

namespace Bifrost.Services
{
	public class ValidationMetaDataService
	{
        ICommandTypeManager _commandTypeManager;
        ICommandValidatorProvider _commandValidatorProvider;

        public ValidationMetaDataService(
            ICommandTypeManager commandTypeManager, 
            ICommandValidatorProvider commandValidatorProvider)
        {
            _commandTypeManager = commandTypeManager;
            _commandValidatorProvider = commandValidatorProvider;
        }

		public void GetForCommand()
		{
            var name = "DoStuffCommand";
            var commandType = _commandTypeManager.GetFromName(name);
            var validator = _commandValidatorProvider.GetInputValidatorFor(commandType) as IValidator;
            if (validator != null)
            {
                var descriptor = validator.CreateDescriptor();
                var validators = descriptor.GetMembersWithValidators();


                
                
            }
		}
	}
}

