/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.CodeGeneration.JavaScript
{
    /// <summary>
    /// Represents a literal
    /// </summary>
    public class Literal : LanguageElement
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Literal"/>
        /// </summary>
        /// <param name="content"><see cref="object"/> representing the content - the actual literal</param>
        public Literal(object content)
        {
            Content = content;
        }

        /// <summary>
        /// Gets or sets the representation of the literal
        /// </summary>
        public object Content { get; set; }

#pragma warning disable 1591
        public override void Write(ICodeWriter writer)
        {
            writer.Write(Content.ToString());
        }
#pragma warning restore 1591
    }
}
