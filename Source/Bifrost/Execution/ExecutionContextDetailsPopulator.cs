#region License
//
// Copyright (c) 2008-2014, Dolittle (http://www.dolittle.com)
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
namespace Bifrost.Execution
{
    /// <summary>
    /// Represents a <see cref="IExecutionContextDetailsPopulator"/>
    /// </summary>
    public class ExecutionContextDetailsPopulator : IExecutionContextDetailsPopulator
    {
        ITypeDiscoverer _typeDiscoverer;
        IContainer _container;
        Type[] _populatorTypes;

        /// <summary>
        /// Initializes an instance of <see cref="ExecutionContextDetailsPopulator"/>
        /// </summary>
        /// <param name="typeDiscoverer"><see cref="ITypeDiscoverer"/> to use for discovering implementations of <see cref="ICanPopulateExecutionContextDetails"/></param>
        /// <param name="container"><see cref="IContainer"/> to use for instantiating types</param>
        public ExecutionContextDetailsPopulator(ITypeDiscoverer typeDiscoverer, IContainer container)
        {
            _typeDiscoverer = typeDiscoverer;
            _container = container;
            _populatorTypes = _typeDiscoverer.FindMultiple<ICanPopulateExecutionContextDetails>();
        }

#pragma warning disable 1591 // Xml Comments
        public void Populate(IExecutionContext executionContext, dynamic details)
        {
            foreach (var type in _populatorTypes)
            {
                var instance = _container.Get(type) as ICanPopulateExecutionContextDetails;
                instance.Populate(executionContext, details);
            }
        }
#pragma warning restore 1591 // Xml Comments
    }
}
