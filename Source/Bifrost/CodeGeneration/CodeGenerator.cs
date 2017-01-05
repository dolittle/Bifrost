/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.IO;
using System.Text;

namespace Bifrost.CodeGeneration
{
    /// <summary>
    /// Represents an implementation for <see cref="ICodeGenerator"/>
    /// </summary>
    public class CodeGenerator : ICodeGenerator
    {
#pragma warning disable 1591
        public string GenerateFrom(ILanguageElement languageElement)
        {
            var stringBuilder = new StringBuilder();
            var textWriter = new StringWriter(stringBuilder);
            var writer = new CodeWriter(textWriter);

            languageElement.Write(writer);

            return stringBuilder.ToString();
        }
#pragma warning restore 1591
    }
}
