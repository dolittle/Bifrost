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
using Bifrost.Execution;

namespace Bifrost.Read
{
    /// <summary>
    /// Represents an implementation of <see cref="IReadModelFilters"/>
    /// </summary>
    public class ReadModelFilters : IReadModelFilters
    {
        ITypeDiscoverer _typeDiscoverer;
        IContainer _container;


        /// <summary>
        /// Initializes an instance of <see cref="ReadModelFilters"/>
        /// </summary>
        /// <param name="typeDiscoverer"><see cref="ITypeDiscoverer"/> to use for discovering filters</param>
        /// <param name="container"><see cref="IContainer"/> for instantiating filters</param>
        public ReadModelFilters(ITypeDiscoverer typeDiscoverer, IContainer container)
        {
            _typeDiscoverer = typeDiscoverer;
            _container = container;
        }

#pragma warning disable 1591
        public IEnumerable<IReadModel> Filter(IEnumerable<IReadModel> readModels)
        {
            throw new NotImplementedException();
        }
#pragma warning restore 1591
    }
}
