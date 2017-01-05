/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.CodeGeneration.JavaScript
{
    /// <summary>
    /// Represents an assignment of a key to an <see cref="ObjectLiteral"/>
    /// </summary>
    public class KeyAssignment : Assignment
    {
        /// <summary>
        /// Initializes a new instance of <see cref="KeyAssignment"/>
        /// </summary>
        /// <param name="key">Key to assign a value</param>
        /// <param name="value"><see cref="ILanguageElement"></see></param>
        public KeyAssignment(string key, ILanguageElement value = null) : base(key, value) { }

#pragma warning disable 1591
        public override void Write(ICodeWriter writer)
        {
            writer.WriteWithIndentation("{0} : ", Name);
            if( Value != null ) Value.Write(writer);
        }
#pragma warning restore 1591
    }
}
