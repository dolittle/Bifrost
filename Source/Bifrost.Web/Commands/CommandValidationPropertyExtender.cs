/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.CodeGeneration.JavaScript;
using Bifrost.Validation.MetaData;
using Newtonsoft.Json;

namespace Bifrost.Web.Commands
{
    public class CommandValidationPropertyExtender : ICanExtendCommandProperty
    {
        IValidationMetaData _validationMetaData;

        public CommandValidationPropertyExtender(IValidationMetaData validationMetaData)
        {
            _validationMetaData = validationMetaData;
        }

        public void Extend(Type commandType, string propertyName, Observable observable)
        {
            var metaData = _validationMetaData.GetMetaDataFor(commandType);
            if (metaData.Properties.ContainsKey(propertyName))
            {
                var options = JsonConvert.SerializeObject(metaData.Properties[propertyName],
                    new JsonSerializerSettings { ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver() });
                observable.ExtendWith("validation", options);
            }
        }
    }
}
