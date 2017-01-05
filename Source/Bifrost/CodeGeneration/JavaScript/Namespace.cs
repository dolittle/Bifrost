/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Extensions;

namespace Bifrost.CodeGeneration.JavaScript
{
    /// <summary>
    /// Represents a namespace, a Bifrost specific construct
    /// </summary>
    public class Namespace : LanguageElement
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Namespace"/>
        /// </summary>
        /// <param name="name">Name of the namespace</param>
        public Namespace(string name)
        {
            Name = name;
            Content = new ObjectLiteral();
            Content.Parent = this;
        }

        /// <summary>
        /// Gets or sets the name of the namespace
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the <see cref="ObjectLiteral"/> representing the content of the namespace
        /// </summary>
        public ObjectLiteral Content { get; private set; }

        /// <summary>
        /// Gets the fully qualified name for a type in this namespace
        /// </summary>
        /// <param name="typeName">Name of a type in this namespace</param>
        /// <returns>Fully qualified name</returns>
        public string GetFullyQualifiedNameForType(string typeName)
        {
            return string.Concat(Name, ".", typeName.ToPascalCase());
        }

#pragma warning disable 1591
        public override void Write(ICodeWriter writer)
        {
            writer.WriteWithIndentation("Bifrost.namespace(\"{0}\", ", Name);
            Content.Write(writer);
            writer.WriteWithIndentation(");");
            writer.Newline();
        }
#pragma warning restore 1591
    }
}
