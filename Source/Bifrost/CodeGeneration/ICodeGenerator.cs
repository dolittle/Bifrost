/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.CodeGeneration
{
    /// <summary>
    /// Defines a code generator
    /// </summary>
    public interface ICodeGenerator
    {
        /// <summary>
        /// Generates the code from a given <see cref="ILanguageElement"/>
        /// </summary>
        /// <param name="languageElement"><see cref="ILanguageElement"/> to generate the code from</param>
        /// <returns><see cref="string"/> containing the generated code</returns>
        string GenerateFrom(ILanguageElement languageElement);
    }
}
