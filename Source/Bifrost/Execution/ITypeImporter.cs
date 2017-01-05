/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Execution
{
	/// <summary>
	/// Defines a container for importing types.
	/// </summary>
	public interface ITypeImporter
	{
		/// <summary>
		/// Import many instances of type
		/// </summary>
		/// <typeparam name="T">Basetype to import - any inheritors will be found and created</typeparam>
		/// <returns>An array of instances that implements or inherits from the given base type</returns>
		T[] ImportMany<T>();

		/// <summary>
		/// Import a single instance of a type
		/// </summary>
		/// <typeparam name="T">Basetype to import</typeparam>
		/// <returns>An instance of a type that implements the given base type</returns>
		T Import<T>();
	}
}
