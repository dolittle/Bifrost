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
namespace Bifrost.Commands
{
    /// <summary>
    /// Defines a builder for building <see cref="ICommand">commands</see>
    /// </summary>
    public interface ICommandBuilder<T> where T:ICommand
    {
        /// <summary>
        /// Gets or sets the name of the <see cref="ICommand"/>being built
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the default parameters for the <see cref="ICommand"/>
        /// </summary>
        dynamic Parameters { get; set; }

        /// <summary>
        /// Gets or sets the constructor parameters for the <see cref="ICommand"/>
        /// </summary>
        /// <remarks>
        /// The order of the parameters must match the constructor.
        /// Also; the amount of parameters must match as well
        /// </remarks>
        object[] ConstructorParameters { get; set; }

        /// <summary>
        /// Gets or sets the type to use for building the <see cref="ICommand"/>
        /// </summary>
        /// <remarks>
        /// Default type will bee <see cref="Command"/>
        /// </remarks>
        Type Type { get; set; }

        /// <summary>
        /// Get an instance of the <see cref="ICommand"/>
        /// </summary>
        /// <returns>An instance of the <see cref="ICommand"/></returns>
        T GetInstance();
    }
}
