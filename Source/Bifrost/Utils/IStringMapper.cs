/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Bifrost.Utils
{
    /// <summary>
    /// Defines a system for mapping strings that picks the first <see cref="IStringMapping"/> that matches
    /// </summary>
    public interface IStringMapper
    {
        /// <summary>
        /// Indicates whether the mapper holds a mapping that matches this string
        /// </summary>
        /// <param name="input">string to check for mapper</param>
        /// <returns>true if it has at least one matching mapper, false otherwise</returns>
        bool HasMappingFor(string input);

        /// <summary>
        /// Gets the first mapper that matches the inputted string
        /// </summary>
        /// <param name="input">string to match against</param>
        /// <returns>The first matching <see cref="IStringMapping"/> for the inputted string</returns>
        IStringMapping GetFirstMatchingMappingFor(string input);

        /// <summary>
        /// Returns all mappers that can resolve the inputted string
        /// </summary>
        /// <param name="input">string to match against</param>
        /// <returns>All <see cref="IStringMapping"/> that match the inputted string</returns>
        IEnumerable<IStringMapping> GetAllMatchingMappingsFor(string input);

        /// <summary>
        /// Resolves the inputted string using the first matching mapper
        /// </summary>
        /// <param name="input">string to resolve</param>
        /// <returns>the resolved string</returns>
        string Resolve(string input);

        /// <summary>
        /// Adds an <see cref="IStringMapping"/> to the mappings
        /// </summary>
        /// <param name="format">Format to map from</param>
        /// <param name="mappedFormat">Format to map to</param>
        void AddMapping(string format, string mappedFormat);

        /// <summary>
        /// Adds an <see cref="IStringMapping"/> to the mappings
        /// </summary>
        /// <param name="mapping">The mapping to add</param>
        void AddMapping(IStringMapping mapping);
    }
}
