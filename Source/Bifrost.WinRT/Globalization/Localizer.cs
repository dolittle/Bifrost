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

using System.Threading;
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

            Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = Configure.Instance.Culture.Name;

            return scope;
        }

        public void EndScope(LocalizationScope scope)
        {
            Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = scope.Culture.Name;
        }
#pragma warning restore 1591 // Xml Comments
    }
}