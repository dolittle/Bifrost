/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.CodeGeneration
{
    /// <summary>
    /// Defines the contract of a language element
    /// </summary>
    public interface ILanguageElement
    {
        /// <summary>
        /// Gets or sets the parent <see cref="ILanguageElement"/>
        /// </summary>
        ILanguageElement Parent { get; set; }

        /// <summary>
        /// Add a child <see cref="ILanguageElement"/> to this
        /// </summary>
        /// <param name="element"><see cref="ILanguageElement"/> to add</param>
        void AddChild(ILanguageElement element);

        /// <summary>
        /// Writes code to the <see cref="ICodeWriter"/>
        /// </summary>
        /// <param name="writer"><see cref="ICodeWriter"/> to write to</param>
        void Write(ICodeWriter writer);
    }
}
