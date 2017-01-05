/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Sagas
{
	/// <summary>
	/// Defines a marker interface to allow transitions between chapters
	/// </summary>
	/// <typeparam name="T">Type of <see cref="IChapter"/> that can be transitioned to</typeparam>
    public interface ICanTransitionTo<T> where T:IChapter
    {
        
    }
}