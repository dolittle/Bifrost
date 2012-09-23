#region License
//
// Copyright (c) 2008-2012, DoLittle Studios AS and Komplett ASA
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
using System.Linq.Expressions;
using System.Reflection;
using Bifrost.Extensions;
using FluentValidation.Validators;

namespace Bifrost.Validation
{
    /// <summary>
    /// Represents a <see cref="PropertyValidator"/> that can hold dynamic state
    /// </summary>
    public abstract class PropertyValidatorWithDynamicState : PropertyValidator
    {
        List<PropertyInfo> _properties = new List<PropertyInfo>();

#pragma warning disable 1591 // Xml Comments
        protected PropertyValidatorWithDynamicState(Expression<Func<string>> errorMessageResourceSelector) : base(errorMessageResourceSelector) { }
        protected PropertyValidatorWithDynamicState(string errorMessage) : base(errorMessage) { }
        protected PropertyValidatorWithDynamicState(string errorMessageResourceName, Type errorMessageResourceType) : base(errorMessageResourceName, errorMessageResourceType) { }
#pragma warning restore 1591 // Xml Comments

        /// <summary>
        /// Properties representing the dynamic state
        /// </summary>
        public IEnumerable<PropertyInfo> Properties { get { return _properties; } }

        /// <summary>
        /// Add an expression that resolve to a property
        /// </summary>
        /// <param name="expression">Expression to add</param>
        public void AddExpression<T>(Expression<Func<T,object>> expression)
        {
            var property = expression.GetPropertyInfo();
            _properties.Add(property);
        }
    }
}
