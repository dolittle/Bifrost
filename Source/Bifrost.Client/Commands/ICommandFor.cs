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
using System.Windows.Input;

namespace Bifrost.Commands
{
    /// <summary>
    /// Represents a <see cref="System.Windows.Input.ICommand"/> for a Bifrost <see cref="ICommand"/>
    /// </summary>
    /// <typeparam name="T">Type of <see cref="ICommand"/> to represent</typeparam>
    /// <remarks>
    /// This is a bridge interface for being able to use the build in functionality of
    /// the XAML platform without taking too many dependencies on infrastructure for 
    /// working with <see cref="ICommand">commands</see>
    /// </remarks>
    public interface ICommandFor<T> : System.Windows.Input.ICommand, ICommandProcess
        where T:Bifrost.Commands.ICommand
    {
        /// <summary>
        /// Gets or sets the instance of the <see cref="ICommand"/>
        /// </summary>
        T Instance { get; set;  }
    }
}
