/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Values
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public class IgnoreChangesAttribute : Attribute
	{
		
	}
}
