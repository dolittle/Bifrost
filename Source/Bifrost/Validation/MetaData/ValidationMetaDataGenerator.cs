#region License
//
// Copyright (c) 2008-2013, Dolittle (http://www.dolittle.com)
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
using System.Reflection;
using Bifrost.Concepts;
using Bifrost.Execution;
using Bifrost.Extensions;
using FluentValidation;
using FluentValidation.Validators;
#if(NETFX_CORE)
using System.Reflection;
#endif

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

            GetValue(inputValidator, metaData, String.Empty);

            return metaData;
        }

        void GetValue(IValidator inputValidator, ValidationMetaData metaData, string parentKey, bool isParentConcept = false, bool isParentModelRule = false)
        {
            var inputValidatorType = inputValidator.GetType();
#if(NETFX_CORE)
            var genericArguments = inputValidatorType.GetTypeInfo().BaseType.GenericTypeArguments;
#else
            var genericArguments = inputValidatorType.BaseType.GetGenericArguments();
#endif

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
                        var currentKey = string.Empty;
                        if (isParentConcept || isParentModelRule || isModelRule)
                            currentKey = parentKey;
                        else
                            currentKey = string.IsNullOrEmpty(parentKey) ? member.Key : string.Format("{0}.{1}", parentKey, member.Key.ToCamelCase());

                        if (validator is ChildValidatorAdaptor)
                        {
                            var isConcept = false;
                            
                            if (genericArguments.Length == 1)
                            {
                                var type = isModelRule ? genericArguments[0] : GetPropertyInfo(genericArguments[0], member.Key).PropertyType;
                                isConcept = type.IsConcept();
                            }

                            var childValidator = (validator as ChildValidatorAdaptor).Validator;
                            GetValue(childValidator, metaData, currentKey, isConcept, isModelRule);
                        }
                        else if (validator is IPropertyValidator)
                        {
                            GenerateFor(metaData, currentKey, validator as IPropertyValidator);
                        }
                    }
                }
            }
        }
#pragma warning restore 1591 // Xml Comments

        void GenerateFor(ValidationMetaData metaData, string property, IPropertyValidator validator)
        {
            var validatorType = validator.GetType();
            var types = new List<Type>();
            types.Add(validatorType);
            types.AddRange(validatorType
#if(NETFX_CORE)
                                    .GetTypeInfo().ImplementedInterfaces
#else
                                    .GetInterfaces()
#endif
                );
            foreach (var type in types)
            {
                if (_generatorsByType.ContainsKey(type))
                {
                    var rule = _generatorsByType[type].GeneratorFrom(validator);
                    var ruleName = rule.GetType().Name.ToCamelCase();
                    var propertyName = property.ToCamelCase();
                    metaData[propertyName][ruleName] = rule;
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

        PropertyInfo GetPropertyInfo(Type type, string name)
        {
        #if(NETFX_CORE)
            return type.GetTypeInfo().GetDeclaredProperty(name);
        #else
            return type.GetProperty(name);
        #endif
        }
    }
}
