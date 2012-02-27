#region License
//
// Copyright (c) 2008-2012, DoLittle Studios and Komplett ASA
//
// Licensed under the Microsoft Permissive License (Ms-PL), Version 1.1 (the "License")
// With one exception :
//   Commercial libraries that is based partly or fully on Bifrost and is sold commercially,
//   must obtain a commercial license.
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at
//
//   http://bifrost.codeplex.com/license
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion
using System;
using System.Collections.Generic;
using Bifrost.Execution;
using Bifrost.Extensions;
using FluentValidation;
using FluentValidation.Validators;

namespace Bifrost.Validation.MetaData
{
    /// <summary>
    /// Represents an implementation of <see cref="IValidationMetaDataGenerator"/>
    /// </summary>
    public class ValidationMetaDataGenerator : IValidationMetaDataGenerator
    {
        ITypeDiscoverer _typeDiscoverer;
        IContainer _container;

        Dictionary<Type, ICanGenerateRule> _generatorsByType = new Dictionary<Type, ICanGenerateRule>();


        /// <summary>
        /// Initializes a new instance of <see cref="ValidationMetaDataGenerator"/>
        /// </summary>
        /// <param name="typeDiscoverer"><see cref="ITypeDiscoverer"/> to use for discovering generators</param>
        /// <param name="container"><see cref="IContainer"/> to use for activation of generators</param>
        public ValidationMetaDataGenerator(ITypeDiscoverer typeDiscoverer, IContainer container)
        {
            _typeDiscoverer = typeDiscoverer;
            _container = container;
            PopulateGenerators();
        }


#pragma warning disable 1591 // Xml Comments
        public ValidationMetaData GenerateFrom(IValidator inputValidator)
        {
            var metaData = new ValidationMetaData();
            var descriptor = inputValidator.CreateDescriptor();
            var members = descriptor.GetMembersWithValidators();
            foreach (var member in members)
            {
                var rules = descriptor.GetRulesForMember(member.Key);
                foreach (var rule in rules)
                {
                    foreach (var validator in rule.Validators)
                    {
                        if (validator is IPropertyValidator)
                            GenerateFor(metaData, member.Key, validator as IPropertyValidator);
                    }
                }
            }

            return metaData;
        }
#pragma warning restore 1591 // Xml Comments

        void GenerateFor(ValidationMetaData metaData, string property, IPropertyValidator validator)
        {
            var validatorType = validator.GetType();
            var types = new List<Type>();
            types.Add(validatorType);
            types.AddRange(validatorType.GetInterfaces());
            foreach (var type in types)
            {
                if (_generatorsByType.ContainsKey(type))
                {
                    var rule = _generatorsByType[type].GeneratorFrom(validator);
                    var ruleName = rule.GetType().Name.ToCamelCase();
                    metaData[property][ruleName] = rule;
                }
            }
        }

        void PopulateGenerators()
        {
            var generatorTypes = _typeDiscoverer.FindMultiple<ICanGenerateRule>();
            foreach (var generatorType in generatorTypes)
            {
                var generator = _container.Get(generatorType) as ICanGenerateRule;
                foreach (var validatorType in generator.From)
                {
                    _generatorsByType[validatorType] = generator;
                }
            }
        }
    }
}
