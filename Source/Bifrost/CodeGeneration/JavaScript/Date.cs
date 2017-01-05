/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.CodeGeneration.JavaScript
{
    /// <summary>
    /// Represents this
    /// </summary>
    public class Date : LanguageElement
    {
#pragma warning disable 1591
        public override void Write(ICodeWriter writer)
        {
            writer.Write("new Date()");
        }
#pragma warning restore 1591
    }
}
