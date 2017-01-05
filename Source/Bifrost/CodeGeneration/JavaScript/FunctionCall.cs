/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.CodeGeneration.JavaScript
{
    /// <summary>
    /// Represents a call to a function
    /// </summary>
    public class FunctionCall : LanguageElement
    {
        /// <summary>
        /// Gets or sets the name of the function to call
        /// </summary>
        public string Function { get; set; }

        /// <summary>
        /// Gets or sets the parameters
        /// </summary>
        public LanguageElement[] Parameters { get; set; }

#pragma warning disable 1591
        public override void Write(ICodeWriter writer)
        {
            writer.Write("{0}(", Function);
            if (Parameters != null && Parameters.Length > 0)
            {
                var count = 0;
                foreach (var parameter in Parameters)
                {
                    if (count != 0) writer.Write(", ");

                    parameter.Write(writer);

                    count++;
                }
            }
            writer.Write(")");
        }
#pragma warning restore 1591
    }
}
