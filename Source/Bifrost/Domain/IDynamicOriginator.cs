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