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
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Bifrost.Commands;

namespace Bifrost.Web.Mvc.Commands
{
    public class ControllerHelpers
    {
        public static MethodInfo GetActionForCommand<T, TC>()
            where T : ICommand, new()
            where TC : ControllerBase
        {
            var controllerType = typeof(TC);
            var commandType = typeof(T);
            var methods = controllerType.GetMethods(BindingFlags.Public | BindingFlags.Instance);
            var actions = methods
                .Where(m => typeof(ActionResult).IsAssignableFrom(m.ReturnType))
                .Where(m => m.GetParameters().Any(p => p.ParameterType == commandType))
                .ToArray();

            ThrowIfAmbiguousAction<T, TC>(actions);
            ThrowIfMissingAction<T, TC>(actions);

            return actions[0];
        }

        static void ThrowIfMissingAction<T, TC>(IEnumerable<MethodInfo> methods)
        {
            if (methods.Count() == 0)
                throw new MissingActionException(typeof(T), typeof(TC));
        }

        static void ThrowIfAmbiguousAction<T, TC>(IEnumerable<MethodInfo> methods)
        {
            if (methods.Count() > 1)
                throw new AmbiguousActionException(typeof(T), typeof(TC));
        }


        public static string GetControllerNameFromType<TC>()
        {
            const string controllerString = "Controller";
            var name = typeof(TC).Name;
            var lastIndex = name.LastIndexOf(controllerString);

            if (lastIndex > 0)
                name = name.Remove(lastIndex, controllerString.Length);

            return name;
        }

    }
}
