/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace Bifrost.Events
{
	/// <summary>
	/// Represents arguments for a <see cref="MethodEvent">MethodEvent</see>
	/// </summary>
	public class MethodEventArguments : DynamicObject
	{
		private readonly Dictionary<string, object> _arguments = new Dictionary<string, object>();

		/// <summary>
		/// Gets or sets the value associated with a given argument for a method
		/// </summary>
		/// <param name="argument">Name of argument</param>
		/// <returns>Value for the argument</returns>
		public object this[string argument]
		{
			get { return _arguments[argument]; }
			set { _arguments[argument] = value; }
		}

		/// <summary>
		/// Get all values for all arguments
		/// </summary>
		/// <returns></returns>
		public object[] GetArgumentValues()
		{
			var parameters = _arguments.Values.ToArray();
			return parameters;
		}


#pragma warning disable 1591 // Xml Comments
		public override bool TrySetMember(SetMemberBinder binder, object value)
		{
			_arguments[binder.Name] = value;
			return true;
		}

		public override bool TryGetMember(GetMemberBinder binder, out object result)
		{
			result = null;
			if( _arguments.ContainsKey(binder.Name))
			{
				result = _arguments[binder.Name];
				return true;
			}

			return false;
		}
#pragma warning restore 1591 // Xml Comments
	}
}