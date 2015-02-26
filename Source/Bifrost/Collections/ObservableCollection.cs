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
    /// Represents an implementation of <see cref="IObservableCollection{T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ObservableCollection<T> : IObservableCollection<T>
    {
        List<T> _internalList = new List<T>();

#pragma warning disable 1591 // Xml Comments
        public event ItemsAddedToCollection<T> Added = (s, e) => { };

        public event ItemsRemovedFromCollection<T> Removed = (s, e) => { };

        public event CollectionCleared<T> Cleared = (s) => { };

        public void Add(T item)
        {
            _internalList.Add(item);
            OnAdded(new[] { item });
        }

        public void Clear()
        {
            _internalList.Clear();
            OnCleared();
        }

        public bool Contains(T item)
        {
            return _internalList.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _internalList.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _internalList.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(T item)
        {
            var result = _internalList.Remove(item);
            OnRemoved(new[] { item });
            return result;
        }

        public System.Collections.Generic.IEnumerator<T> GetEnumerator()
        {
            return _internalList.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _internalList.GetEnumerator();
        }
#pragma warning restore 1591 // Xml Comments

        void OnAdded(IEnumerable<T> items)
        {
            Added(this, items);
        }

        void OnRemoved(IEnumerable<T> items)
        {
            Removed(this, items);
        }

        void OnCleared()
        {
            Cleared(this);
        }
    }
}
