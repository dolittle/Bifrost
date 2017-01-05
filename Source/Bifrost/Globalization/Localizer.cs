/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Globalization;
using Bifrost.Configuration;

namespace Bifrost.Globalization
{
    /// <summary>
    /// Represents a <see cref="ILocalizer"/>
    /// </summary>
	public class Localizer : ILocalizer
    {
#pragma warning disable 1591 // Xml Comments
        public LocalizationScope BeginScope()
		{
			var scope = LocalizationScope.FromCurrentThread();

			CultureInfo.CurrentCulture = Configure.Instance.Culture;
			CultureInfo.CurrentUICulture = Configure.Instance.UICulture;

			return scope;
		}

		public void EndScope(LocalizationScope scope)
		{
			CultureInfo.CurrentCulture = scope.Culture;
			CultureInfo.CurrentUICulture = scope.UICulture;
        }
#pragma warning restore 1591 // Xml Comments
    }
}