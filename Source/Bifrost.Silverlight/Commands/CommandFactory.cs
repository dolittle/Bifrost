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
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Reflection;
using Bifrost.Extensions;

namespace Bifrost.Commands
{
    /// <summary>
    /// Represents a <see cref="ICommandFactory"/> for creating commands with
    /// </summary>
    public class CommandFactory : ICommandFactory
    {
        ICommandCoordinator _commandCoordinator;
        ICommandBuildingConventions _conventions;

        /// <summary>
        /// Initializes a new instance of <see cref="CommandFactory"/>
        /// </summary>
        /// <param name="commandCoordinator"><see cref="ICommandCoordinator"/> to use when handling commands</param>
        public CommandFactory(ICommandCoordinator commandCoordinator, ICommandBuildingConventions conventions)
        {
            _commandCoordinator = commandCoordinator;
            _conventions = conventions;
        }

#pragma warning disable 1591 // Xml Comments
        public ICommandBuilder<TC> BuildFrom<TC>(Expression<Func<TC>> property, ICommandBuildingConventions conventions = null) where TC:ICommand
        {
            if (conventions == null) conventions = _conventions;

            var propertyName = property.GetPropertyInfo().Name;
            var builder = BuildFromName<TC>(propertyName, conventions);
            return builder;
        }

        public ICommandBuilder<TC> BuildFor<TC>() where TC : ICommand, new()
        {
            var builder = new CommandBuilder<TC>(_commandCoordinator, _conventions);
            builder.WithName(typeof(TC).Name);
            return builder;
        }

        public void BuildAndPopulateAll<T>(T target, ICommandBuildingConventions conventions = null)
        {
            if (conventions == null) conventions = _conventions;

            ICommand instance = null;
            foreach (var property in GetCommandProperties(typeof(T)))
            {
                if (property.GetValue(target, null) == null)
                {
                    if (property.PropertyType == typeof(ICommand))
                        instance = BuildFromName<Command>(property.Name, conventions).GetInstance();
                    else
                    {
                        var builder = CreateCommandBuilderFor(property.PropertyType);
                        instance = (ICommand)builder.GetType().GetMethod("GetInstance").Invoke(builder,null);
                    }
                    property.SetValue(target, instance, null);
                }
            }
            
        }
#pragma warning restore 1591 // Xml Comments

        object CreateCommandBuilderFor(Type commandType)
        {
            var genericType = typeof(CommandBuilder<>).MakeGenericType(commandType);
            var builder = Activator.CreateInstance(genericType, _commandCoordinator, _conventions);
            return builder;
        }


        CommandBuilder<TC> BuildFromName<TC>(string name, ICommandBuildingConventions conventions) where TC:ICommand
        {
            var builder = new CommandBuilder<TC>(_commandCoordinator, _conventions);
            builder.WithName(conventions.CommandName(name));
            return builder;
        }

        IEnumerable<PropertyInfo> GetCommandProperties(Type type)
        {
            return type.GetProperties().Where(p => p.PropertyType.HasInterface<ICommand>() || p.PropertyType == typeof(ICommand));
        }


    }
}
