/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Linq;
using System.Reflection;
using Ninject.Injection;
using Ninject.Planning.Targets;

namespace Bifrost.Ninject
{
    /// <summary>
    /// Represents an implementation of <see cref="global::Ninject.Planning.Directives.ConstructorInjectionDirective"/> for
    /// fixing an activation bug inside Ninject (https://github.com/ninject/Ninject/issues/235)
    /// </summary>
    public class ConstructorInjectionDirective : global::Ninject.Planning.Directives.ConstructorInjectionDirective
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="constructor"></param>
        /// <param name="injector"></param>
        public ConstructorInjectionDirective(ConstructorInfo constructor, ConstructorInjector injector)
            : base(constructor, injector)
        {

        }

        /// <inheritdoc/>
        protected override ITarget[] CreateTargetsFromParameters(ConstructorInfo method)
        {
            return method.GetParameters().Select(parameter => new ParameterTarget(method, parameter)).ToArray();
        }

    }
}
