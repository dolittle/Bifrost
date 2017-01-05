/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.CodeGeneration.JavaScript
{
    /// <summary>
    /// Represents a function
    /// </summary>
    public class Function : LanguageElement
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Function"/>
        /// </summary>
        /// <param name="parameters">Optional parameters for the function</param>
        public Function(params string[] parameters)
        {
            Parameters = parameters;
            Body = new FunctionBody();
        }

        /// <summary>
        /// Gets or sets the parameters for the function
        /// </summary>
        public string[] Parameters { get; set; }

        /// <summary>
        /// Gets the <see cref="FunctionBody"/>
        /// </summary>
        public FunctionBody Body { get; private set; }

#pragma warning disable 1591
        public override void Write(ICodeWriter writer)
        {
            writer.Write("function(");
            for (var parameterIndex = 0; parameterIndex < Parameters.Length; parameterIndex++)
            {
                if (parameterIndex != 0)
                    writer.Write(", ");

                writer.Write(Parameters[parameterIndex]);
            }
            writer.Write(") {{");
            writer.Newline();
            Body.Write(writer);
            writer.WriteWithIndentation("}}");
        }
#pragma warning restore 1591
    }
}
