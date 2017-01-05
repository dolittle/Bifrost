/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.CodeGeneration.JavaScript
{
    /// <summary>
    /// Represents an extension to an <see cref="Observable"/>
    /// </summary>
    /// <remarks>
    /// Extensions augment a Knockout observable with new functionality and has access directly to the observable and its value
    /// </remarks>
    public class ObservableExtension
    {
        /// <summary>
        /// Name of extension
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Options in JSON for the extension that gets passed to the extension during creation
        /// </summary>
        public string Options { get; set; }
    }
}
