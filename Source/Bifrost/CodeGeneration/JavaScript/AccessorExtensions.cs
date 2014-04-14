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
namespace Bifrost.CodeGeneration.JavaScript
{
    /// <summary>
    /// Provides methods for working with accessors
    /// </summary>
    public static class AccessorExtensions
    {
        /// <summary>
        /// Call function on an accessor
        /// </summary>
        /// <param name="accessor"><see cref="Accessor"/> perform call on</param>
        /// <param name="callback"><see cref="Action{FunctionCall}"/> that gets called to build the functioncall</param>
        /// <returns>The <see cref="Assignment"/> to build on</returns>
        public static Accessor WithFunctionCall(this Accessor accessor, Action<FunctionCall> callback)
        {
            var functionCall = new FunctionCall();
            accessor.Child = functionCall;
            callback(functionCall);
            return accessor;
        }
    }
}
