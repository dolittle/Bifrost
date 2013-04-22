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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Bifrost.Concepts;
using Bifrost.Extensions;

namespace Bifrost.CodeGeneration.JavaScript
{
    /// <summary>
    /// Provides methods for working with the <see cref="ICodeGenerator"/>
    /// </summary>
    public static class CodeGeneratorExtensions
    {
        /// <summary>
        /// Start a Bifrost namespace
        /// </summary>
        /// <param name="generator"><see cref="ICodeGenerator"/> to create from</param>
        /// <param name="name">Name of namespace</param>
        /// <param name="callback"><see cref="Action{ObjectLiteral}"/> that gets called to build the object literal for the namespace</param>
        /// <returns><see cref="Namespace"/> that is built</returns>
        public static Namespace Namespace(this ICodeGenerator generator, string name, Action<ObjectLiteral> callback)
        {
            var ns = new Namespace(name);
            callback(ns.Content);
            return ns;
        }

        /// <summary>
        /// Start a container for Global namespace
        /// </summary>
        /// <param name="generator"><see cref="ICodeGenerator"/> to create from</param>
        /// <returns><see cref="Global"/> namespace to build from</returns>
        public static Global Global(this ICodeGenerator generator)
        {
            var global = new Global();
            return global;
        }
    }
}
