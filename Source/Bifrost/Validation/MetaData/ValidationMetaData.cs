/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Execution;

namespace Bifrost.Validation.MetaData
{
    /// <summary>
    /// Represents an implementation of <see cref="IValidationMetaData"/>
    /// </summary>
    public class ValidationMetaData : IValidationMetaData
    {
        IInstancesOf<ICanGenerateValidationMetaData> _generators;

        /// <summary>
        /// Initializes an instance of <see cref="ValidationMetaData"/>
        /// </summary>
        public ValidationMetaData(IInstancesOf<ICanGenerateValidationMetaData> generators)
        {
            _generators = generators;
        }

#pragma warning disable 1591 // Xml Comments
        public TypeMetaData GetMetaDataFor(Type typeForValidation)
        {
            var typeMetaData = new TypeMetaData();

            foreach (var generator in _generators)
            {
                var metaData = generator.GenerateFor(typeForValidation);

                foreach (var property in metaData.Properties.Keys)
                {
                    foreach( var ruleSet in metaData.Properties[property].Keys ) 
                    {
                        typeMetaData[property][ruleSet] = metaData.Properties[property][ruleSet];
                    }
                }                
            }

            return typeMetaData;
        }
#pragma warning restore 1591 // Xml Comments
    }
}
