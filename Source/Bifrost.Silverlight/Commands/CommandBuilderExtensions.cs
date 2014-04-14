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
using System.Dynamic;

namespace Bifrost.Commands
{
    /// <summary>
    /// Provides methods for building commands, extensions not available already on <see cref="ICommandBuilder"/>
    /// </summary>
    public static class CommandBuilderExtensions
    {
        /// <summary>
        /// Gives a command being built a name
        /// </summary>
        /// <param name="commandBuilder"><see cref="ICommandBuilder"/> to build on</param>
        /// <param name="name">Name of command</param>
        /// <returns>Chainable <see cref="ICommandBuilder"/></returns>
        public static ICommandBuilder<TC> WithName<TC>(this ICommandBuilder<TC> commandBuilder, string name) where TC:ICommand
        {
            commandBuilder.Name = name;
            return commandBuilder;
        }


        /// <summary>
        /// Indicates that the command builder should build with a specific type in mind
        /// </summary>
        /// <typeparam name="TC">Type of <see cref="ICommand"/> to build</typeparam>
        /// <param name="commandBuilder"><see cref="ICommandBuilder"/> to build on</param>
        /// <returns>Chainable <see cref="ICommandBuilder"/></returns>
        public static ICommandBuilder<TC> WithType<TC>(this ICommandBuilder<TC> commandBuilder, Type type) where TC : ICommand
        {
            commandBuilder.Type = type;
            return commandBuilder;
        }

        /// <summary>
        /// Gives default parameters to a command being built
        /// </summary>
        /// <param name="commandBuilder"><see cref="ICommandBuilder"/> to build on</param>
        /// <param name="parameters">Default parameters to use</param>
        /// <returns>Chainable <see cref="ICommandBuilder"/></returns>
        public static ICommandBuilder<TC> WithParameters<TC>(this ICommandBuilder<TC> commandBuilder, dynamic parameters) where TC : ICommand
        {
            commandBuilder.Parameters = parameters;
            return commandBuilder;
        }


        /// <summary>
        /// Populate the default parameters to a command being build
        /// </summary>
        /// <param name="commandBuilder"><see cref="ICommandBuilder"/> to build on</param>
        /// <param name="populateParameters"><see cref="Action"/> that gets called for populating the parameters</param>
        /// <returns>Chainable <see cref="ICommandBuilder"/></returns>
        public static ICommandBuilder<TC> WithParameters<TC>(this ICommandBuilder<TC> commandBuilder, Action<dynamic> populateParameters) where TC:ICommand
        {
            commandBuilder.Parameters = new ExpandoObject();
            populateParameters(commandBuilder.Parameters);
            return commandBuilder;
        }

        /// <summary>
        /// Gives a command being built, its constructor parameters for when it gets created
        /// </summary>
        /// <typeparam name="TC"></typeparam>
        /// <param name="commandBuilder"><see cref="ICommandBuilder"/> to build on</param>
        /// <param name="constructorParameters">The constructor parameters used when creating an instance of the <see cref="ICommand"/></param>
        /// <returns>Chainable <see cref="ICommandBuilder"/></returns>
        /// <remarks>
        /// The order of the parameters must match the constructor.
        /// Also; the amount of parameters must match as well
        /// </remarks>
        public static ICommandBuilder<TC> WithConstructorParameters<TC>(this ICommandBuilder<TC> commandBuilder, params object[] constructorParameters) where TC : ICommand
        {
            commandBuilder.ConstructorParameters = constructorParameters;
            return commandBuilder;
        }

    }
}
