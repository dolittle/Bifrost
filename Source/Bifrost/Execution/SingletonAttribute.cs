/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Execution
{
	/// <summary>
	/// Indicates that a class is Singleton and should be treated as such
	/// for any factory creating an instance of a class marked with this
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class SingletonAttribute : Attribute
	{
	}
}