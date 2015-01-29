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
using System;
using System.Linq.Expressions;

namespace Bifrost.Commands
{
    /// <summary>
    /// Defines a factory for creating <see cref="ICommand">commands</see>
    /// </summary>
    public interface ICommandFactory
    {
        /// <summary>
        /// Build a command from a given property
        /// </summary>
        /// <param name="property"><see cref="Expression"/> representing the property that will represent the command</param>
        /// <param name="conventions">Optional conventions for how it will build <see cref="ICommand">commands</see>, if not specified, it will use the default settings</param>
        /// <returns><see cref="ICommandBuilder"/> to use for building the command</returns>
        ICommandBuilder<TC> BuildFrom<TC>(Expression<Func<TC>> property, ICommandBuildingConventions conventions = null) where TC : ICommand;


        /// <summary>
        /// Build a command for a known command
        /// </summary>
        /// <typeparam name="TC"><see cref="ICommand"/> to build for</typeparam>
        /// <returns><see cref="ICommandBuilder"/> to use for building the command</returns>
        ICommandBuilder<TC> BuildFor<TC>() where TC : ICommand, new();

        /// <summary>
        /// Build all <see cref="ICommand">commands</see> based on conventions and populate the properties with the instance
        /// of the command
        /// </summary>
        /// <typeparam name="T">Type of the target</typeparam>
        /// <param name="target">Target to build from and populate to</param>
        /// <param name="conventions">Optional conventions for how it will build <see cref="ICommand">commands</see>, if not specified, it will use the default settings</param>
        void BuildAndPopulateAll<T>(T target, ICommandBuildingConventions conventions = null);
    }
}
