/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Execution;
using Ninject.Syntax;
#if(FALSE)
using Ninject.Web.Common;
#endif


namespace Bifrost.Ninject
{
	/// <summary>
	/// Provides functionality for setting scope to a Ninject <see cref="IBindingInSyntax{T}">Binding syntax</see> 
	/// </summary>
	public static class BindingLifecycleExtensions
	{

		/// <summary>
		/// Set scope based on <see cref="BindingLifecycle">ActivationScope</see>
		/// </summary>
		/// <param name="syntax"><see cref="IBindingInSyntax{T}">Binding syntax</see> to set the scope for</param>
		/// <param name="lifecycle"><see cref="BindingLifecycle"/> to use</param>
        public static void WithLifecycle<T>(this IBindingInSyntax<T> syntax, BindingLifecycle lifecycle)
		{
			switch (lifecycle)
			{
				case BindingLifecycle.Singleton:
					syntax.InSingletonScope();
					break;
#if(false)
				case BindingLifecycle.Request:
					syntax.InRequestScope();
					break;

				case BindingLifecycle.Thread:
					syntax.InThreadScope();
					break;
#endif			
				case BindingLifecycle.Transient:
					syntax.InTransientScope();
					break;
			}
		}
	}
}