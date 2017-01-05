/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Globalization;

namespace Bifrost.Globalization
{
    /// <summary>
    /// Represents a scope for localization, when exiting the scope, cultures will be reset back to the cultures given at construction.
    /// The scopes purpose is to enable one to change cultures within a given scope but have it gracefully reset back to the previous
    /// or a given culture when disposed
    /// </summary>
	public class LocalizationScope : IDisposable
	{
        /// <summary>
        /// Gets the culture for the <see cref="LocalizationScope"/>
        /// </summary>
		public CultureInfo Culture { get; private set; }

        /// <summary>
        /// Gets the UI culture for the <see cref="LocalizationScope"/>
        /// </summary>
		public CultureInfo UICulture { get; private set; }

        /// <summary>
        /// Initializes a new instance of <see cref="LocalizationScope"/>
        /// </summary>
        /// <param name="culture"><see cref="CultureInfo"/> to initialize the scope with</param>
        /// <param name="uiCulture"><see cref="CultureInfo"/> to initialize the scope as the UI culture with</param>
		public LocalizationScope(CultureInfo culture, CultureInfo uiCulture)
		{
			Culture = culture;
			UICulture = uiCulture;
		}

        /// <summary>
        /// Get current <see cref="LocalizationScope"/> from the current thread
        /// </summary>
        /// <returns></returns>
		public static LocalizationScope	FromCurrentThread()
		{
			var scope = new LocalizationScope(CultureInfo.CurrentCulture, CultureInfo.CurrentUICulture);
			return scope;
		}

        /// <summary>
        /// Dispose the scope, resetting the culture back to the cultures given at construction
        /// </summary>
		public void Dispose()
		{
			CultureInfo.CurrentCulture = Culture;
			CultureInfo.CurrentUICulture = UICulture;
		}
	}
}