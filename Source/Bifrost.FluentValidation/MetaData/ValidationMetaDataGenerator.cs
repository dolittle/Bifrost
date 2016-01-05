#region License
//
// Copyright (c) 2008-2015, Dolittle (http://www.dolittle.com)
//
// Licensed under the MIT License (http://opensource.org/licenses/MIT)
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at 
//
//   http://github.com/dolittle/Bifrost/blob/master/MIT-LICENSE.txt
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
using System.Linq;
using System.Reflection;
using Bifrost.Concepts;
using Bifrost.Execution;
using Bifrost.Extensions;
using Bifrost.FluentValidation.Commands;
using Bifrost.Validation.MetaData;
using FluentValidation;
using FluentValidation.Validators;

namespace Bifrost.FluentValidation.MetaData
{
    /// <summary>
    /// Represents an implementation of <see cref="ICanGenerateValidationMetaData"/>
    /// </summary>
    public class ValidationMetaDataGenerator : ICanGenerateValidationMetaData
    {
        ICommandValidatorProvider _validatorProvider;
        Dictionary<Type, ICanGenerateRule> _generatorsByType;

        /// <summary>
        /// Initializes a new instance of <see cref="ValidationMetaDataGenerator"/>
        /// </summary>
        /// <param name="ruleGenerators">The known instances of generators.</param>
        /// <param name="validatorProvider">The provider of command input validators.</param>
        public ValidationMetaDataGenerator(IInstancesOf<ICanGenerateRule> ruleGenerators, ICommandValidatorProvider validatorProvider)
        {
            _validatorProvider = validatorProvider;
            _generatorsByType = Generators(ruleGenerators);
        }

#pragma warning disable 1591 // Xml Comments

        public TypeMetaData GenerateFor(Type typeForValidation)
        {
            var metaData = new TypeMetaData();

            var validator = _validatorProvider.GetInputValidatorFor(typeForValidation);
            GenerateForValidator(validator, metaData, string.Empty);

            return metaData;
        }


        void GenerateForValidator(IValidator inputValidator, TypeMetaData metaData, string parentKey, bool isParentConcept = false, bool isParentModelRule = false)
        {
            var inputValidatorType = inputValidator.GetType();
            var genericArguments = inputValidatorType.BaseType.GetGenericArguments();

            var descriptor = inputValidator.CreateDescriptor();
            var members = descriptor.GetMembersWithValidators();
            
            foreach (var member in members)
            {
                var rules = descriptor.GetRulesForMember(member.Key);
                foreach (var rule in rules)
                {
                    foreach (var validator in rule.Validators)
                    {
                        var isModelRule = member.Key == ModelRule<string>.ModelRulePropertyName;
                        var currentKey = GetKeyForMember(parentKey, isParentConcept, isParentModelRule, member, isModelRule);

                        if (validator is ChildValidatorAdaptor)
                        {
                            GenerateForChildValidator(metaData, genericArguments, member, validator, isModelRule, currentKey);
                        }
                        else if (validator is IPropertyValidator)
                        {
                            GenerateFor(metaData, currentKey, validator);
                        }
                    }
                }
            }
        }


#pragma warning restore 1591 // Xml Comments
        string GetKeyForMember(string parentKey, bool isParentConcept, bool isParentModelRule, IGrouping<string, IPropertyValidator> member, bool isModelRule)
        {
            var currentKey = string.Empty;
            if (isParentConcept || isParentModelRule || isModelRule)
                currentKey = parentKey;
            else
                currentKey = string.IsNullOrEmpty(parentKey) ? member.Key : string.Format("{0}.{1}", parentKey, member.Key.ToCamelCase());
            return currentKey;
        }

        void GenerateForChildValidator(TypeMetaData metaData, Type[] genericArguments, IGrouping<string, IPropertyValidator> member, IPropertyValidator validator, bool isModelRule, string currentKey)
        {
            var isConcept = false;

            if (genericArguments.Length == 1)
            {
                var type = isModelRule ? genericArguments[0] : GetPropertyInfo(genericArguments[0], member.Key).PropertyType;
                isConcept = type.IsConcept();
            }

            var childValidator = (validator as ChildValidatorAdaptor).Validator;
            GenerateForValidator(childValidator, metaData, currentKey, isConcept, isModelRule);
        }

        void GenerateFor(TypeMetaData metaData, string property, IPropertyValidator validator)
        {
            var validatorType = validator.GetType();
            var types = new List<Type>();
            types.Add(validatorType);
            types.AddRange(validatorType.GetInterfaces());
            foreach (var type in types)
            {
                if (_generatorsByType.ContainsKey(type))
                {
                    var propertyName = property.ToCamelCase();
                    var rule = _generatorsByType[type].GeneratorFrom(property, validator);
                    var ruleName = rule.GetType().Name.ToCamelCase();
                    metaData[propertyName][ruleName] = rule;
                }
            }
        }

        Dictionary<Type, ICanGenerateRule> Generators(IInstancesOf<ICanGenerateRule> ruleGenerators)
        {
            return (
                from generator in ruleGenerators
                from type in generator.From
                select new {generator, type})
                .ToDictionary(d => d.type, d => d.generator);
        }

        PropertyInfo GetPropertyInfo(Type type, string name)
        {
            return type.GetProperty(name);
        }
    }
}
