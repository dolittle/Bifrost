/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Domain
{
	/// <summary>
	/// Defines an origin, typically for aggregated roots when needing to create mementos and set them
	/// 
	/// This interface represents the mementos dynamically
	/// </summary>
	public interface IDynamicOriginator
	{
		/// <summary>
		/// Create a memento
		/// </summary>
		/// <returns>Dynamic representation of the memento</returns>
		dynamic CreateMemento();

		/// <summary>
		/// Set a memento
		/// </summary>
		/// <param name="memento">Dynamic representation of the memento to set</param>
		void SetMemento(dynamic memento);
	}
}