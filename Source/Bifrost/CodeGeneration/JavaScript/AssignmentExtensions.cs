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
using System;

namespace Bifrost.CodeGeneration.JavaScript
{
    /// <summary>
    /// Provides methods for working with assignment
    /// </summary>
    public static class AssignmentExtensions
    {
        /// <summary>
        /// Assign a key within an <see cref="ObjectLiteral"/>
        /// </summary>
        /// <param name="objectLiteral"><see cref="ObjectLiteral"/> to assign to</param>
        /// <param name="name">Name of key</param>
        /// <returns><see cref="KeyAssignment"/> to build</returns>
        public static KeyAssignment Assign(this ObjectLiteral objectLiteral, string name)
        {
            var keyAssignment = new KeyAssignment(name);
            objectLiteral.AddChild(keyAssignment);
            return keyAssignment;
        }

        /// <summary>
        /// Assign "this" to an <see cref="Assignment"/>
        /// </summary>
        /// <param name="assignment"><see cref="Assignment"/> to assign to</param>
        /// <returns>The <see cref="Assignment"/> to build on</returns>
        public static Assignment WithThis(this Assignment assignment)
        {
            assignment.Value = new This();
            return assignment;
        }

        /// <summary>
        /// Assign a literal
        /// </summary>
        /// <param name="assignment"><see cref="Assignment"/> to assign to</param>
        /// <param name="literal">Literal to assign</param>
        /// <returns>The <see cref="Assignment"/> to build on</returns>
        public static Assignment WithLiteral(this Assignment assignment, object literal)
        {
            assignment.Value = new Literal(literal);
            return assignment;
        }

        /// <summary>
        /// Assign a string
        /// </summary>
        /// <param name="assignment"><see cref="Assignment"/> to assign to</param>
        /// <param name="theString">String to assign</param>
        /// <returns>The <see cref="Assignment"/> to build on</returns>
        public static Assignment WithString(this Assignment assignment, string theString)
        {
            assignment.WithLiteral(string.Format("\"{0}\"", theString));
            return assignment;
        }


        /// <summary>
        /// Assign a type
        /// </summary>
        /// <param name="assignment"><see cref="Assignment"/> to assign to</param>
        /// <param name="callback"><see cref="Action{TypeExtension}"/> that gets called to build the type</param>
        /// <returns>The <see cref="Assignment"/> to build on</returns>
        public static Assignment WithType(this Assignment assignment, Action<TypeExtension> callback)
        {
            var typeExtension = new TypeExtension();
            assignment.Value = typeExtension;
            callback(typeExtension);
            return assignment;
        }

        /// <summary>
        /// Assign a function
        /// </summary>
        /// <param name="assignment"><see cref="Assignment"/> to assign to</param>
        /// <param name="callback"><see cref="Action{Function}"/> that gets called to build the function</param>
        /// <param name="parameters">Optional parameters for the function</param>
        /// <returns>The <see cref="Assignment"/> to build on</returns>
        public static Assignment WithFunction(this Assignment assignment, Action<Function> callback, params string[] parameters)
        {
            var function = new Function(parameters);
            assignment.Value = function;
            callback(function);
            return assignment;
        }

        /// <summary>
        /// Assign a function call
        /// </summary>
        /// <param name="assignment"><see cref="Assignment"/> to assign to</param>
        /// <param name="callback"><see cref="Action{FunctionCall}"/> that gets called to build the functioncall</param>
        /// <returns>The <see cref="Assignment"/> to build on</returns>
        public static Assignment WithFunctionCall(this Assignment assignment, Action<FunctionCall> callback)
        {
            var functionCall = new FunctionCall();
            assignment.Value = functionCall;
            callback(functionCall);
            return assignment;
        }

        /// <summary>
        /// Assign an observable
        /// </summary>
        /// <param name="assignment"><see cref="Assignment"/> to assign to</param>
        /// <param name="defaultValue">Optional default value</param>
        /// <returns>The <see cref="Assignment"/> to build on</returns>
        public static Assignment WithObservable(this Assignment assignment, string defaultValue = null)
        {
            return assignment.WithFunctionCall(f =>
            {
                f.WithName("ko.observable");
                if (defaultValue != null) f.WithParameters(defaultValue);
            });
        }


        /// <summary>
        /// Assign an observable array
        /// </summary>
        /// <param name="assignment"><see cref="Assignment"/> to assign to</param>
        /// <returns>The <see cref="Assignment"/> to build on</returns>
        public static Assignment WithObservableArray(this Assignment assignment)
        {
            return assignment.WithFunctionCall(f => f.WithName("ko.observableArray"));
        }

        /// <summary>
        /// Assign an object literal
        /// </summary>
        /// <param name="assignment"><see cref="Assignment"/> to assign to</param>
        /// <param name="callback"><see cref="Action{ObjectLiteral}"/> that gets called to build the object literal</param>
        /// <returns>The <see cref="Assignment"/> to build on</returns>
        public static Assignment WithObjectLiteral(this Assignment assignment, Action<ObjectLiteral> callback = null)
        {
            var objectLiteral = new ObjectLiteral();
            if (callback != null) callback(objectLiteral);
            assignment.Value = objectLiteral;
            return assignment;
        }
    }
}
