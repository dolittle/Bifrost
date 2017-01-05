/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;
using Bifrost.Extensions;
using FluentValidation;
using FluentValidation.Validators;

namespace Bifrost.FluentValidation
{
    /// <summary>
    /// Combines multiples validator descriptors into a single validator descriptor.
    /// </summary>
    public class ComposedValidatorDescriptor : IValidatorDescriptor
    {
        readonly IList<IValidatorDescriptor> _registeredDescriptors;

        /// <summary>
        /// Instantiates an instance of a <see cref="ComposedValidatorDescriptor"/>
        /// </summary>
        /// <param name="registeredDescriptors"></param>
        public ComposedValidatorDescriptor(IEnumerable<IValidatorDescriptor> registeredDescriptors)
        {
            _registeredDescriptors = registeredDescriptors.ToList();
        }

        /// <summary>
        /// Gets the name display name for a property.
        /// </summary>
        public string GetName(string property)
        {
            return _registeredDescriptors
                .Select(d => d.GetName(property))
                .Where(name => name != null)
                .FirstOrDefault();
        }

        /// <summary>
        /// Gets a collection of validators grouped by property.
        /// </summary>
        public ILookup<string, IPropertyValidator> GetMembersWithValidators()
        {
            return _registeredDescriptors
                .Select(d => d.GetMembersWithValidators())
                .Combine();
        }

        /// <summary>
        /// Gets validators for a particular property.
        /// </summary>
        public IEnumerable<IPropertyValidator> GetValidatorsForMember(string name)
        {
            return _registeredDescriptors
                .SelectMany(d => d.GetValidatorsForMember(name));
        }

        /// <summary>
        /// Gets rules for a property.
        /// </summary>
        public IEnumerable<IValidationRule> GetRulesForMember(string name)
        {
            return _registeredDescriptors
                .SelectMany(d => d.GetRulesForMember(name));
        }
    }
}