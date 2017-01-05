/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
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
