/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Mapping
{
    /// <summary>
    /// Defines the strategy for mapping properties
    /// </summary>
    public interface IPropertyMappingStrategy
    {
        /// <summary>
        /// Performs the mapping
        /// </summary>
        /// <param name="mappingTarget">Mapping target to use</param>
        /// <param name="target">Target in which to map to</param>
        /// <param name="value">Value from to set</param>
        void Perform(IMappingTarget mappingTarget, object target, object value);
    }
}
