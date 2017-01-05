/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.CodeGeneration.JavaScript
{
    /// <summary>
    /// Represents an accessor
    /// </summary>
    public class Accessor : LanguageElement
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Accessor"/>
        /// </summary>
        /// <param name="name">Name of accessor</param>
        public Accessor(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Gets the name of the <see cref="Accessor"/>
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets or sets any child of the <see cref="Accessor"/>
        /// </summary>
        public ILanguageElement Child { get; set; }

#pragma warning disable 1591
        public override void Write(ICodeWriter writer)
        {
            writer.WriteWithIndentation("{0}", Name);
            if (Child != null)
            {
                writer.Write(".");
                Child.Write(writer);
            }

            writer.Newline();
        }
#pragma warning restore 1591
    }
}
