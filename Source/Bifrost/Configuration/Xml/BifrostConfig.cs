#region License
//
// Copyright (c) 2008-2013, Dolittle (http://www.dolittle.com)
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
namespace Bifrost.Configuration.Xml
{
	/// <summary>
	/// Represents the entire configuration of Bifrost from Xml
	/// </summary>
    public class BifrostConfig
    {
		/// <summary>
		/// Gets or sets the default storage configuration, typically applied if not explicitly configured elsewhere
		/// </summary>
        public StorageElement DefaultStorage { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="CommandsElement"/> for configuring commands behavior in Bifrost
		/// </summary>
        public CommandsElement Commands { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="EventsElement"/> for configuring events behavior in Bifrost
		/// </summary>
        public EventsElement Events { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="SagasElement"/> for configuring sagas behavior in Bifrost
		/// </summary>
		public SagasElement Sagas { get; set; }
    }
}