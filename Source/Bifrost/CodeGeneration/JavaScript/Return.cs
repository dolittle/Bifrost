/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.CodeGeneration.JavaScript
{
    /// <summary>
    /// Represents a return statement
    /// </summary>
    public class Return : LanguageElement
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Literal"/>
        /// </summary>
        /// <param name="returnValue"><see cref="LanguageElement"/> representing the content - the actual literal</param>
        public Return(LanguageElement returnValue)
        {
            ReturnValue = returnValue;
        }

        /// <summary>
        /// Gets or sets the representation of the literal
        /// </summary>
        public LanguageElement ReturnValue { get; set; }

#pragma warning disable 1591
        public override void Write(ICodeWriter writer)
        {
            writer.WriteWithIndentation("return ");
            ReturnValue.Write(writer);
            writer.Newline();
        }
#pragma warning restore 1591
    }
}
