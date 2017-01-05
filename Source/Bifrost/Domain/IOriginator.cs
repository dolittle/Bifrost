/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Domain
{
	/// <summary>
	/// Defines an origin, typically for aggregated roots when needing to create mementos and set them
	/// </summary>
	public interface IOriginator
	{
		/// <summary>
		/// Create memento
		/// </summary>
		/// <returns>The actual memento</returns>
		IMemento CreateMemento();

		/// <summary>
		/// Set mememoty
		/// </summary>
		/// <param name="memento">The actual memento to set</param>
		void SetMemento(IMemento memento);
	}
}
