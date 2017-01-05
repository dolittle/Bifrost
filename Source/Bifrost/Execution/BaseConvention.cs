/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq;
using System.Reflection;

namespace Bifrost.Execution
{
	/// <summary>
	/// Defines a base abstract class for Binding conventions for any <see cref="IContainer"/>
	/// </summary>
	public abstract class BaseConvention : IBindingConvention
	{
		/// <summary>
		/// Gets or sets the <see cref="BindingLifecycle">ActivationScope</see> that will be used as default
		/// </summary>
		public BindingLifecycle DefaultScope { get; set; }

#pragma warning disable 1591 // Xml Comments
		public abstract bool CanResolve(IContainer container, Type service);
		public abstract void Resolve(IContainer container, Type service);
#pragma warning restore 1591 // Xml Comments


		/// <summary>
		/// Handle scope for a target type
		/// </summary>
		/// <param name="targetType">Target type</param>
		/// <returns><see cref="BindingLifecycle"/> for the target type</returns>
		/// <remarks>
		/// If the target is marked with the <see cref="SingletonAttribute">Singleton</see> attribute, it will use
		/// that scope instead, as that is a explicit implementation information.
		/// 
		/// Otherwise it will use the DefaultScope
		/// </remarks>
        protected BindingLifecycle GetScopeForTarget(Type targetType)
		{
            var attributes = targetType.GetTypeInfo().GetCustomAttributes(typeof(SingletonAttribute), false).ToArray();
            return attributes.Length == 1 ? BindingLifecycle.Singleton : DefaultScope;
		}
	}
}