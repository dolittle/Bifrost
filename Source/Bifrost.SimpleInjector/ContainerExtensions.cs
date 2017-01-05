/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Execution;
using SimpleInjector;
using SimpleInjector.Integration.Web;


namespace Bifrost.SimpleInjector
{
    public static class ContainerExtensions
    {
        public static void Register<T>(this global::SimpleInjector.Container container, Func<T> resolveCallback, BindingLifecycle lifecycle)
        {
            Func<Type> typeResolver = () => { return resolveCallback.Invoke().GetType(); };
            container.Register(typeof(T), typeResolver, lifecycle);
        }
        public static void Register<T>(this global::SimpleInjector.Container container, Func<Type> resolveCallback, BindingLifecycle lifecycle)
        {
            container.Register(typeof(T), resolveCallback, lifecycle);
        }
        public static void Register(this global::SimpleInjector.Container container, Type service, Func<Type> resolveCallback, BindingLifecycle lifecycle)
        {
            var lifestyle = ResolveLifestyle(lifecycle);
            container.Register(service, resolveCallback, lifestyle);
        }

        private static Lifestyle ResolveLifestyle(BindingLifecycle lifecycle)
        {
            var lifestyle = Lifestyle.Transient;
            switch (lifecycle)
            {
                case BindingLifecycle.Singleton:
                    lifestyle = Lifestyle.Singleton;
                    break;
                case BindingLifecycle.Request:
                    lifestyle = new WebRequestLifestyle();
                    break;
                case BindingLifecycle.Transient:
                    lifestyle = Lifestyle.Transient;
                    break;
                case BindingLifecycle.Thread:
                    throw new NotSupportedException("Ref documentation: This lifestyle is deliberately left out of Simple Injector because it is considered to be harmful. Instead of using Per Thread lifestyle, you will usually be better of using one of the Scoped lifestyles.");
            }

            return lifestyle;
        }


    }
}
