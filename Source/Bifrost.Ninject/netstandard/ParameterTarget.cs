/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Reflection;
using Ninject.Planning.Targets;

namespace Bifrost.Ninject
{
    /// <summary>
    /// Represents an implementation of <see cref="Target{T}"/> for working around a bug in
    /// Ninject (https://github.com/ninject/Ninject/issues/235) related to HasDefaultValue 
    /// returning true when it should be false
    /// </summary>
    public class ParameterTarget : Target<ParameterInfo>
    {
        Lazy<object> _defaultValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterTarget"/> class.
        /// </summary>
        /// <param name="method">The method that defines the parameter.</param>
        /// <param name="site">The parameter that this target represents.</param>
        public ParameterTarget(MethodBase method, ParameterInfo site)
            : base(method, site)
        {
            _defaultValue = new Lazy<object>(() => site.DefaultValue);
        }

        /// <inheritdoc/>
        public override string Name
        {
            get { return Site.Name; }
        }

        /// <inheritdoc/>
        public override Type Type
        {
            get { return Site.ParameterType; }
        }

        /// <inheritdoc/>
        public override bool HasDefaultValue => Site.HasDefaultValue;

        /// <inheritdoc/>
        public override object DefaultValue
        {
            get { return HasDefaultValue ? _defaultValue.Value : base.DefaultValue; }
        }
    }
}
