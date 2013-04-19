#region License
//
// Copyright (c) 2008-2013, Dolittle (http://www.dolittle.com)
//
// Licensed under the MIT License (http://opensource.org/licenses/MIT)
// With one exception :
//   Commercial libraries that is based partly or fully on Bifrost and is sold commercially,
//   must obtain a commercial license.
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
using System.Web.Mvc;
using Bifrost.Commands;

namespace Bifrost.Web.Mvc.Commands
{
    /// <summary>
    /// The exception that is thrown if the <see cref="CommandHtmlHelper"/> or <see cref="CommandAjaxHelper"/>
    /// can't find a unique action to use for your command when creating a <see cref="CommandForm"/>
    /// </summary>
    public class AmbiguousActionException : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of <see cref="AmbiguousActionException"/>
        /// </summary>
        /// <param name="commandType">Type of <see cref="ICommand"/></param>
        /// <param name="controllerType">Type of <see cref="IController"/></param>
        public AmbiguousActionException(Type commandType, Type controllerType)
            : base(string.Format("Can't find a unique action for command of type '{0}' on controller '{0}'", commandType.AssemblyQualifiedName, controllerType.AssemblyQualifiedName))
        {
        }
    }
}
