/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.CodeGeneration.JavaScript
{
    /// <summary>
    /// Represents the global scope
    /// </summary>
    public class Global : FunctionBody
    {
#pragma warning disable 1591
        public override void Write(ICodeWriter writer)
        {
            WriteChildren(writer);
        }
#pragma warning restore 1591
    }
}
