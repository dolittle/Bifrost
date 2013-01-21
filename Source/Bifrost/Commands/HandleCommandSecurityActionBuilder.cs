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
using Bifrost.Security;

namespace Bifrost.Commands
{
    /// <summary>
    /// Represents a <see cref="ISecurityActionBuilder"/> for handling of <see cref="ICommand">commands</see>
    /// </summary>
    public class HandleCommandSecurityActionBuilder : SecurityActionBuilder<HandleCommand>
    {
        /// <summary>
        /// Initializes an instance of <see cref="HandleCommandSecurityActionBuilder"/>
        /// </summary>
        /// <param name="action"><see cref="HandleCommand"/> we are building for</param>
        public HandleCommandSecurityActionBuilder(HandleCommand action) : base(action)
        { 
        }

        /// <summary>
        /// Add a <see cref="CommandSecurableBuilder"/> to the <see cref="HandleCommandSecurityActionBuilder"/>
        /// </summary>
        /// <returns><see cref="CommandSecurableBuilder"/></returns>
        public CommandSecurityTargetBuilder Commands()
        {
            var target = new CommandSecurityTarget();
            var targetBuilder = new CommandSecurityTargetBuilder(target);
            Action.AddTarget(target);
            return targetBuilder;
        }
    }
}
