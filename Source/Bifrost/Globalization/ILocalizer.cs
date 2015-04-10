#region License
//
// Copyright (c) 2008-2015, Dolittle (http://www.dolittle.com)
//
// Licensed under the MIT License (http://opensource.org/licenses/MIT)
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
