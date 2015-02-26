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
using System.Collections.Generic;

namespace Bifrost.Collections
{
    /// <summary>
    /// Represents the delegate that gets called when items are added to a collection
    /// </summary>
    /// <typeparam name="T">Type of item in the collection</typeparam>
    /// <param name="collection"><see cref="IObservableCollection{T}"/> that received the items</param>
    /// <param name="items">The items</param>
    public delegate void ItemsAddedToCollection<T>(IObservableCollection<T> collection, IEnumerable<T> items);
}
