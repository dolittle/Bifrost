/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

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
        /// <returns><see cref="Bifrost.CodeGeneration.JavaScript.Namespace"/> that is built</returns>
        public static Namespace Namespace(this ICodeGenerator generator, string name, Action<ObjectLiteral> callback)
        {
            var ns = generator.Namespace(name);
            callback(ns.Content);
            return ns;
        }

        /// <summary>
        /// Start a Bifrost namespace
        /// </summary>
        /// <param name="generator"><see cref="ICodeGenerator"/> to create from</param>
        /// <param name="name">Name of namespace</param>
        /// <returns><see cref="Bifrost.CodeGeneration.JavaScript.Namespace"/> that is built</returns>
        public static Namespace Namespace(this ICodeGenerator generator, string name)
        {
            var ns = new Namespace(name);
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
