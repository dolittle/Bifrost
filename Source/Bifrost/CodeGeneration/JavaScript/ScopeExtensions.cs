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

namespace Bifrost.CodeGeneration.JavaScript
{
    /// <summary>
    /// Provide methods for working with <see cref="Scope"/>
    /// </summary>
    public static class ScopeExtensions 
    {
        /// <summary>
        /// Specify a <see cref="FunctionCall"/> for the <see cref="Scope"/>
        /// </summary>
        /// <param name="scope"><see cref="Scope"/> to specify for</param>
        /// <param name="callback"><see cref="Action{FunctionCall}"/> that gets called for setting up the <see cref="FunctionCall"/></param>
        /// <returns>Chained <see cref="Scope"/> to keep building on</returns>
        public static Scope FunctionCall(this Scope scope, Action<FunctionCall> callback)
        {
            var functionCall = new FunctionCall();
            scope.AddChild(functionCall);
            callback(functionCall);
            return scope;
        }
    }
}
