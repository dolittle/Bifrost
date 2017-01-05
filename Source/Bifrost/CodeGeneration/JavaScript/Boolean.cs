/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.CodeGeneration.JavaScript
{
    /// <summary>
    /// Represents this
    /// </summary>
    public class Boolean : LanguageElement
    {

        /// <summary>
        /// Initializes a new instance of <see cref="Boolean"/>
        /// </summary>
        /// <param name="value"><see cref="bool"/> representing the value of the boolean</param>
        public Boolean(bool value)
        {
            Value = value;
        }

        /// <summary>
        /// Gets or sets the representation of the lboolean
        /// </summary>
        public bool Value { get; set; }

#pragma warning disable 1591
        public override void Write(ICodeWriter writer)
        {
            writer.Write("{0}",Value.ToString().ToLower());
        }
#pragma warning restore 1591
    }
}
