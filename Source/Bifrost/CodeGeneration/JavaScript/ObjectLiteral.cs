/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.CodeGeneration.JavaScript
{
    /// <summary>
    /// Represents an object literal
    /// </summary>
    public class ObjectLiteral : Container
    {
#pragma warning disable 1591
        public override void Write(ICodeWriter writer)
        {
            var childIndex = 0;
            var numberOfChildren = Children.Count;

            writer.Write("{{");

            if (numberOfChildren > 0)
            {
                writer.Newline();
                writer.Indent();

                foreach (var child in Children)
                {
                    child.Write(writer);
                    childIndex++;

                    if (childIndex > 0 && childIndex < numberOfChildren)
                        writer.Write(",");
                    writer.Newline();
                }

                writer.Unindent();
                writer.WriteWithIndentation("}}");
            }
            else
            {
                writer.Write("}}");
            }
        }
#pragma warning restore 1591
    }
}
