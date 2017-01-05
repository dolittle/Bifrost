/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Bifrost.CodeGeneration.JavaScript
{
    /// <summary>
    /// Represents a body of a <see cref="Function"/>
    /// </summary>
    public class FunctionBody : Container
    {
#pragma warning disable 1591
        public override void Write(ICodeWriter writer)
        {
            writer.Indent();
            WriteChildren(writer);
            writer.Unindent();
        }
#pragma warning restore 1591
    }
}
