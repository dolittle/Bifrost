/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.CodeGeneration.JavaScript
{
    /// <summary>
    /// Represents a Bifrost specific Type extension
    /// </summary>
    public class TypeExtension : Container
    {
        /// <summary>
        /// Initializes a new instance of <see cref="TypeExtension"/>
        /// </summary>
        /// <param name="superType">Super type that is being extended, default is "Bifrost.Type"</param>
        public TypeExtension(string superType="Bifrost.Type")
        {
            SuperType = superType;
            Function = new Function();
        }

        /// <summary>
        /// Gets or sets the type being extended
        /// </summary>
        public string SuperType { get; set; }

        /// <summary>
        /// Gets the function representing the type
        /// </summary>
        public Function Function { get; private set; }

#pragma warning disable 1591
        public override void Write(ICodeWriter writer)
        {
            writer.Write("{0}.extend(", SuperType);
            Function.Write(writer);
            writer.Write(")");
        }
#pragma warning restore 1591
    }
}
