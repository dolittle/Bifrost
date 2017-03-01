/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;

namespace Bifrost.Utils
{
    /// <summary>
    /// Represents an implementation of <see cref="IStringMapper"/>
    /// </summary>
    public class StringMapper : IStringMapper
    {
        List<IStringMapping> _mappings = new List<IStringMapping>();

#pragma warning disable 1591 // Xml Comments
        public IEnumerable<IStringMapping> Mappings => _mappings;

        public bool HasMappingFor(string input)
        {
            return Mappings.Any(mapping => mapping.Matches(input));
        }

        public IStringMapping GetFirstMatchingMappingFor(string input)
        {
            return Mappings.FirstOrDefault(mapping => mapping.Matches(input));
        }

        public IEnumerable<IStringMapping> GetAllMatchingMappingsFor(string input)
        {
            return Mappings.Where(mapping => mapping.Matches(input));
        }

        public string Resolve(string input)
        {
            return GetFirstMatchingMappingFor(input)?.Resolve(input);
        }

        public void AddMapping(string format, string mappedFormat)
        {
            _mappings.Add(new StringMapping(format, mappedFormat));
        }

        public void AddMapping(IStringMapping mapping)
        {
            _mappings.Add(mapping);
        }
#pragma warning restore 1591 // Xml Comments
    }
}
