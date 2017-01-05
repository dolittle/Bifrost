/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Globalization
{
    /// <summary>
    /// Defines a localizer for entering in and out of a <see cref="LocalizationScope"/>
    /// </summary>
	public interface ILocalizer
	{
        /// <summary>
        /// Begin a <see cref="LocalizationScope"/>
        /// </summary>
        /// <returns><see cref="LocalizationScope"/></returns>
		LocalizationScope BeginScope();

        /// <summary>
        /// End a <see cref="LocalizationScope"/>
        /// </summary>
        /// <param name="scope"><see cref="LocalizationScope"/> to end</param>
		void EndScope(LocalizationScope scope);
	}
}
