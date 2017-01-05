/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.CodeGeneration.JavaScript
{
    /// <summary>
    /// Represents a scope, typically used when you want to assign something within a scope
    /// </summary>
    public class Scope : LanguageElement
    {
        /// <summary>
        /// Initializes a new instance of a <see cref="Scope"/>
        /// </summary>
        /// <param name="name">Name of scope</param>
        public Scope(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Gets or sets the name of the scope
        /// </summary>
        public string Name { get; set; }


#pragma warning disable 1591
        public override void Write(ICodeWriter writer)
        {
            foreach (var child in Children)
            {
                writer.WriteWithIndentation("{0}.", Name);
                child.Write(writer);
                writer.Write(";");
                writer.Newline();
            }
        }
#pragma warning restore 1591
    }
}
