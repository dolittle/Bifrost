/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Utils
{
    /// <summary>
    /// Defines a mapping for strings
    /// </summary>
    public interface IStringMapping
    {
        /// <summary>
        /// Gets the source format
        /// </summary>
        string Format { get; }

        /// <summary>
        /// Gets the mapped format
        /// </summary>
        string MappedFormat { get; }

        /// <summary>
        /// Checks wether or not a particular input string matches the format for the mapping
        /// </summary>
        /// <param name="input">String to check</param>
        /// <returns>True if matches, false it not</returns>
        bool Matches(string input);

        /// <summary>
        /// Get expanded values from a string
        /// </summary>
        /// <param name="input">String to get from</param>
        /// <returns>A dynamic object holding the values from the string</returns>
        dynamic GetValues(string input);

        /// <summary>
        /// Resolves an input string using the format and mapped format
        /// </summary>
        /// <param name="input">String to resolve</param>
        /// <returns>Resolved string</returns>
        string Resolve(string input);
    }
}
