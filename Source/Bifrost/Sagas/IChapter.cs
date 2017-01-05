/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Sagas
{
	/// <summary>
	/// Defines a chapter in a <see cref="ISaga"/>
	/// </summary>
    public interface IChapter
	{
        /// <summary>
        /// Lifecycle method for a <see cref="IChapter"/>, gets called when the <see cref="IChapter"/> is created
        /// </summary>
	    void OnCreated();

        /// <summary>
        /// Lifecycle method for a <see cref="IChapter"/>, gets called when the <see cref="IChapter"/> is set as current
        /// </summary>
	    void OnSetCurrent();

        /// <summary>
        /// Lifecycle method for a <see cref="IChapter"/>, gets called when the <see cref="IChapter"/> is transitioned to
        /// </summary>
	    void OnTransitionedTo();
	}
}