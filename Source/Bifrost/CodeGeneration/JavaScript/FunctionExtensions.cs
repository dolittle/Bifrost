/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq;

namespace Bifrost.CodeGeneration.JavaScript
{
    /// <summary>
    /// Provides methods for working with <see cref="Function"/> and related objects
    /// </summary>
    public static class FunctionExtensions
    {
        /// <summary>
        /// Specify parameters for a function
        /// </summary>
        /// <param name="function"><see cref="Function"/> to specify for</param>
        /// <param name="parameters">Parameters for the function</param>
        /// <returns>Chained <see cref="Function"/> to keep building on</returns>
        public static Function WithParameters(this Function function, params string[] parameters)
        {
            function.Parameters = parameters;
            return function;
        }


        /// <summary>
        /// Add a accessor accessing an object
        /// </summary>
        /// <param name="functionBody"><see cref="FunctionBody"/> to add to</param>
        /// <param name="name">Name of variant</param>
        /// <param name="callback"><see cref="Action{Accessor}"/> that gets called for working with the <see cref="Accessor"/></param>
        /// <returns>Chained <see cref="FunctionBody"/> to keep building on</returns>
        public static FunctionBody Access(this FunctionBody functionBody, string name, Action<Accessor> callback)
        {
            var accessor = new Accessor(name);
            functionBody.AddChild(accessor);
            callback(accessor);
            return functionBody;
        }

        /// <summary>
        /// Add a property to a <see cref="FunctionBody"/>
        /// </summary>
        /// <param name="functionBody"><see cref="FunctionBody"/> to add to</param>
        /// <param name="name">Name of the property to add</param>
        /// <param name="callback"><see cref="Action{PropertyAssignment}"/> that gets called for working with the <see cref="PropertyAssignment"/></param>
        /// <returns>Chained <see cref="FunctionBody"/> to keep building on</returns>
        public static FunctionBody Property(this FunctionBody functionBody, string name, Action<PropertyAssignment> callback)
        {
            var propertyAssignment = new PropertyAssignment(name);
            functionBody.AddChild(propertyAssignment);
            callback(propertyAssignment);
            return functionBody;
        }

        /// <summary>
        /// Add a <see cref="AccessorAssignment"/> to the <see cref="FunctionBody"/>
        /// </summary>
        /// <param name="functionBody"><see cref="FunctionBody"/> to add to</param>
        /// <param name="name">Name of the property to add</param>
        /// <param name="callback"><see cref="Action{AccessorAssignment}"/> that gets called for working with the <see cref="AccessorAssignment"/></param>
        /// <returns>Chained <see cref="FunctionBody"/> to keep building on</returns>
        public static FunctionBody AssignAccessor(this FunctionBody functionBody, string name, Action<AccessorAssignment> callback)
        {
            var accessorAssignment = new AccessorAssignment(name);
            functionBody.AddChild(accessorAssignment);
            callback(accessorAssignment);
            return functionBody;
        }

        /// <summary>
        /// Add a variant to a <see cref="FunctionBody"/>
        /// </summary>
        /// <param name="functionBody"><see cref="FunctionBody"/> to add to</param>
        /// <param name="name">Name of variant</param>
        /// <param name="callback"><see cref="Action{VariantAssignment}"/> that gets called for working with the <see cref="VariantAssignment"/></param>
        /// <returns>Chained <see cref="FunctionBody"/> to keep building on</returns>
        public static FunctionBody Variant(this FunctionBody functionBody, string name, Action<VariantAssignment> callback)
        {
            var variantAssignment = new VariantAssignment(name);
            functionBody.AddChild(variantAssignment);
            callback(variantAssignment);
            return functionBody;
        }

        /// <summary>
        /// Add a scope - such as "self", typically used together with an <see cref="Assignment"/>
        /// </summary>
        /// <param name="functionBody"><see cref="FunctionBody"/> to add to</param>
        /// <param name="name">Name of the scope, e.g. "self"</param>
        /// <param name="callback"><see cref="Action{Scope}"/> that gets called for working with the <see cref="Scope"/></param>
        /// <returns>Chained <see cref="FunctionBody"/> to keep building on</returns>
        public static FunctionBody Scope(this FunctionBody functionBody, string name, Action<Scope> callback)
        {
            var scope = new Scope(name);
            functionBody.AddChild(scope);
            callback(scope);
            return functionBody;
        }

        /// <summary>
        /// Add a return statement
        /// </summary>
        /// <param name="functionBody"><see cref="FunctionBody"/> to add to</param>
        /// <param name="returnValue"><see cref="LanguageElement"/> representing the returnvalue</param>
        /// <returns>Chained <see cref="FunctionBody"/> to keep building on</returns>
        public static FunctionBody Return(this FunctionBody functionBody, LanguageElement returnValue)
        {
            var returnStatement = new Return(returnValue);
            functionBody.AddChild(returnStatement);
            return functionBody;
        }

        /// <summary>
        /// Set the parameters for a <see cref="FunctionCall"/> based on strings
        /// </summary>
        /// <param name="functionCall"><see cref="FunctionCall"/> to set for</param>
        /// <param name="parameters">Parameters to set</param>
        /// <returns>Chained <see cref="FunctionCall"/> to keep building on</returns>
        public static FunctionCall WithParameters(this FunctionCall functionCall, params string[] parameters)
        {
            functionCall.Parameters = parameters.Select(p=>new Literal(p)).ToArray();
            return functionCall;
        }

        /// <summary>
        /// Set the parameters for a <see cref="FunctionCall"/> 
        /// </summary>
        /// <param name="functionCall"><see cref="FunctionCall"/> to set for</param>
        /// <param name="parameters">Parameters to set</param>
        /// <returns>Chained <see cref="FunctionCall"/> to keep building on</returns>
        public static FunctionCall WithParameters(this FunctionCall functionCall, params LanguageElement[] parameters)
        {
            functionCall.Parameters = parameters;
            return functionCall;
        }

        /// <summary>
        /// Specify a name for the <see cref="FunctionCall"/>
        /// </summary>
        /// <param name="functionCall"><see cref="FunctionCall"/> to set name for</param>
        /// <param name="name">Name of the <see cref="FunctionCall"/></param>
        /// <returns>Chained <see cref="FunctionCall"/> to keep building on</returns>
        public static FunctionCall WithName(this FunctionCall functionCall, string name)
        {
            functionCall.Function = name;
            return functionCall;
        }
    }
}
