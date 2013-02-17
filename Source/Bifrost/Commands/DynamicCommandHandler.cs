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
using Bifrost.Domain;
using Bifrost.Execution;

namespace Bifrost.Commands
{
	/// <summary>
	/// Represents a <see cref="ICommandHandler"/> that works on <see cref="DynamicCommand"/>
	/// </summary>
    public class DynamicCommandHandler : ICommandHandler
    {
        IContainer _container;

        /// <summary>
        /// Initializes a new instance of <see cref="DynamicCommandHandler"/>
        /// </summary>
        /// <param name="container"></param>
        public DynamicCommandHandler(IContainer container)
        {
            _container = container;
        }

        static Type GetTypeFor(Type typeToGet, Type aggregatedRootType)
        {
            var genericFactoryType = typeToGet.MakeGenericType(aggregatedRootType);
            return genericFactoryType;
        }

#pragma warning disable 1591 // Xml Comments

		public void Handle(DynamicCommand command)
        {
            var factoryType = GetTypeFor(typeof(IAggregatedRootFactory<>),command.AggregatedRootType);
            var factory = _container.Get(factoryType) as IAggregatedRootFactory;

            var repositoryType = GetTypeFor(typeof(IAggregatedRootRepository<>), command.AggregatedRootType);
            var repository = _container.Get(repositoryType) as IAggregatedRootRepository;

            AggregatedRoot aggregatedRoot;
            try
            {
                aggregatedRoot = repository.Get(command.Id) as AggregatedRoot;
            } catch( MissingAggregatedRootException )
            {
                aggregatedRoot = factory.Create(command.Id) as AggregatedRoot;
            }

            command.InvokeMethod(aggregatedRoot);
		}
#pragma warning restore 1591 // Xml Comments

	}
}