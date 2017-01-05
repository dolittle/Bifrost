/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
namespace Bifrost.CodeGeneration.JavaScript
{
    /// <summary>
    /// Represents an observable property and the assignment of this
    /// </summary>
    public class Observable : FunctionCall
    {
        List<ObservableExtension> _extensions;

        /// <summary>
        /// Initializes a new instance of <see cref="Observable"/>
        /// </summary>
        /// <param name="defaultValue">Optional default value to initialize the observable with</param>
        public Observable(object defaultValue = null)
        {
            Function = "ko.observable";
            if( defaultValue != null ) 
                Parameters = new Literal[] { new Literal(defaultValue) };

            _extensions = new List<ObservableExtension>();
        }

        /// <summary>
        /// Gets any extensions added to the <see cref="Observable"/>
        /// </summary>
        public IEnumerable<ObservableExtension> Extensions { get { return _extensions; } }


        /// <summary>
        /// Add an extension to the <see cref="Observable"/>
        /// </summary>
        /// <param name="name">Name of extension</param>
        /// <param name="options">Any options for the extension in JSON format</param>
        public void AddExtension(string name, string options)
        {
            _extensions.Add(new ObservableExtension
            {
                Name = name,
                Options = options
            });
        }

#pragma warning disable 1591
        public override void Write(ICodeWriter writer)
        {
            base.Write(writer);
            if( _extensions.Count > 0 ) 
            {
                writer.Newline();
                IndentMultiple(writer, 3);
                writer.WriteWithIndentation(".extend({{");
                writer.Newline();
                writer.Indent();

                foreach (var extension in _extensions)
                {
                    if (extension != _extensions[0]) 
                    {
                        writer.Write(",");
                        writer.Newline();
                    }
                    writer.WriteWithIndentation("{0} : {1}", extension.Name, extension.Options);
                }
                writer.Unindent();
                writer.Newline();
                writer.WriteWithIndentation("}})");
                UnindentMultiple(writer, 3);
            }
        }

        void IndentMultiple(ICodeWriter writer, int numberOfIdentions)
        {
            for (var i = 0; i < numberOfIdentions; i++) writer.Indent();
        }

        void UnindentMultiple(ICodeWriter writer, int numberOfIdentions)
        {
            for (var i = 0; i < numberOfIdentions; i++) writer.Unindent();
        }
#pragma warning restore 1591
    }
}
