/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.IO;

namespace Bifrost.CodeGeneration
{
    /// <summary>
    /// Represents an implementation of <see cref="ICodeWriter"/>
    /// </summary>
    public class CodeWriter : ICodeWriter
    {
        int _indentLevel;
        TextWriter _actualWriter;

#pragma warning disable 1591
        public CodeWriter(TextWriter writer)
        {
            _actualWriter = writer;
        }

        public void Indent()
        {
            _indentLevel++;
        }

        public void Unindent()
        {
            _indentLevel--;
        }

        public void WriteWithIndentation(string format, params object[] args)
        {
            for (var i = 0; i < _indentLevel; i++) _actualWriter.Write("\t");
            Write(format, args);
        }

        public void Write(string format, params object[] args)
        {
            _actualWriter.Write(format, args);
        }

        public void Newline()
        {
            _actualWriter.Write("\n");
        }
#pragma warning restore 1591
    }
}
