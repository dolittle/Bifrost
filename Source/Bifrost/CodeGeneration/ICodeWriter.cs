/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.CodeGeneration
{
    /// <summary>
    /// Defines a writer for writing code to, typically used by <see cref="ILanguageElement">language elements</see>
    /// </summary>
    public interface ICodeWriter
    {
        /// <summary>
        /// Increase indentation
        /// </summary>
        void Indent();

        /// <summary>
        /// Decrease indentation
        /// </summary>
        void Unindent();

        /// <summary>
        /// Write string with indentation applied
        /// </summary>
        /// <param name="format"><see cref="string"/> format</param>
        /// <param name="args">Args used by the format string</param>
        void WriteWithIndentation(string format, params object[] args);

        /// <summary>
        /// Write string without indentation applied
        /// </summary>
        /// <param name="format"><see cref="string"/> format</param>
        /// <param name="args">Args used by the format string</param>
        void Write(string format, params object[] args);

        /// <summary>
        /// Add a newline 
        /// </summary>
        void Newline();
    }
}
