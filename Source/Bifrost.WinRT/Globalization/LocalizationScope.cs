#region License
//
// Copyright (c) 2008-2013, Dolittle (http://www.dolittle.com)
//
// Licensed under the MIT License (http://opensource.org/licenses/MIT)
// With one exception :
//   Commercial libraries that is based partly or fully on Bifrost and is sold commercially, 
//   must obtain a commercial license.
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at 
//
//   http://github.com/dolittle/Bifrost/blob/master/MIT-LICENSE.txt
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion

using System;
using System.Globalization;
using System.Threading;

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
        public static LocalizationScope FromCurrentThread()
        {
            var culture = new CultureInfo(Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride);
            var scope = new LocalizationScope(culture, culture);
            return scope;
        }

        /// <summary>
        /// Dispose the scope, resetting the culture back to the cultures given at construction
        /// </summary>
        public void Dispose()
        {
            Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = Culture.Name;
        }
    }
}