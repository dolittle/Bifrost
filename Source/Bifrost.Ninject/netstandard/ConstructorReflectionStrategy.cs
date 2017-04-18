/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Reflection;
using Ninject.Components;
using Ninject.Infrastructure.Language;
using Ninject.Injection;
using Ninject.Planning;
using Ninject.Planning.Strategies;
using Ninject.Selection;

namespace Bifrost.Ninject
{
    /// <summary>
    /// Represents an implementation of <see cref="IPlanningStrategy"/> to work around a bug related
    /// to wether or not a parameter has default value inside Ninject (https://github.com/ninject/Ninject/issues/235)
    /// </summary>
    public class ConstructorReflectionStrategy : NinjectComponent, IPlanningStrategy
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ConstructorReflectionStrategy"/>
        /// </summary>
        /// <param name="selector"><see cref="ISelector"/> to use</param>
        /// <param name="injectorFactory"><see cref="IInjectorFactory"/> to use</param>
        public ConstructorReflectionStrategy(ISelector selector, IInjectorFactory injectorFactory)
        {
            Selector = selector;
            InjectorFactory = injectorFactory;
        }

        /// <inheritdoc/>
        public ISelector Selector { get; private set; }

        /// <inheritdoc/>
        public IInjectorFactory InjectorFactory { get; private set; }

        /// <inheritdoc/>
        public void Execute(IPlan plan)
        {
            var constructors = this.Selector.SelectConstructorsForInjection(plan.Type);
            if (constructors == null)
            {
                return;
            }

            foreach (ConstructorInfo constructor in constructors)
            {
                var hasInjectAttribute = constructor.HasAttribute(this.Settings.InjectAttribute);
                var directive = new ConstructorInjectionDirective(constructor, this.InjectorFactory.Create(constructor))
                {
                    HasInjectAttribute = hasInjectAttribute
                };

                plan.Add(directive);
            }
        }

    }
}
