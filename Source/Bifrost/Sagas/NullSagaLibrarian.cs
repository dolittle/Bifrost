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
using System;
using System.Collections.Generic;

namespace Bifrost.Sagas
{
    /// <summary>
    /// Represents a null implementation of <see cref="ISagaLibrarian"/>
    /// </summary>
    public class NullSagaLibrarian : ISagaLibrarian
    {
#pragma warning disable 1591 // Xml Comments
        public void Close(ISaga saga)
        {
        }

        public void Catalogue(ISaga saga)
        {
        }

        public ISaga Get(Guid id)
        {
            return null;
        }

        public ISaga Get(string partition, string key)
        {
            return null;
        }

        public IEnumerable<ISaga> GetForPartition(string partition)
        {
            return new ISaga[0];
        }
#pragma warning restore 1591 // Xml Comments
    }
}
