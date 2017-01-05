/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
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
