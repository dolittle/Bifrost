#region License
//
// Copyright (c) 2008-2012, DoLittle Studios AS and Komplett ASA
//
// Licensed under the Microsoft Permissive License (Ms-PL), Version 1.1 (the "License")
// With one exception :
//   Commercial libraries that is based partly or fully on Bifrost and is sold commercially, 
//   must obtain a commercial license.
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at 
//
//   http://bifrost.codeplex.com/license
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
using Microsoft.Practices.ServiceLocation;

namespace Bifrost.Commands
{
	/// <summary>
	/// Represents a <see cref="ICommandHandler"/> that works on <see cref="DynamicCommand"/>
	/// </summary>
    public class DynamicCommandHandler : ICommandHandler
    {
        static Type GetTypeFor(Type typeToGet, Type aggregatedRootType)
        {
            var genericFactoryType = typeToGet.MakeGenericType(aggregatedRootType);
            return genericFactoryType;
        }

#pragma warning disable 1591 // Xml Comments

		public void Handle(DynamicCommand command)
        {
            var factoryType = GetTypeFor(typeof(IAggregatedRootFactory<>),command.AggregatedRootType);
            var factory = ServiceLocator.Current.GetInstance(factoryType) as IAggregatedRootFactory;

            var repositoryType = GetTypeFor(typeof(IAggregatedRootRepository<>), command.AggregatedRootType);
            var repository = ServiceLocator.Current.GetInstance(repositoryType) as IAggregatedRootRepository;

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