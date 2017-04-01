/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Strings;

namespace Bifrost.Applications
{
    /// <summary>
    /// Defines a  
    /// </summary>
    public interface IApplicationStructureConfigurationBuilder
    {
        /// <summary>
        /// Include a given mapping for structure representation
        /// </summary>
        /// <param name="area">A unique identifier for the format</param>
        /// <param name="format">Format of the structure</param>
        /// <returns></returns>
        /// <remarks>
        /// The format is based on well known identifiers which is recognized
        /// 
        /// BoundedContext
        /// Module
        /// Feature
        /// SubFeature
        /// 
        /// All these has to be represented as a variable according to the <see cref="IStringFormatParser"/>
        /// 
        /// Example:
        /// 
        /// "{BoundedContext}.-^{Module}.-^{Feature}.-^{SubFeature}*
        /// 
        /// This is telling that the BoundedContext is required, while the rest are optionals and the last
        /// SubFeature is recursive and also depending on the existence of a Feature. SubFeatures belong
        /// to a Feature.
        /// 
        /// One format *MUST* include a BoundedContext - there can only be one of it.
        /// One format *CAN* include Module, and there *MUST* only be one of it
        /// One format *CAN* include Feature, but only if there is a Module before in the string format, and there *MUST* only be one of it
        /// One format *CAN* include multiple SubFeatures, but only if there is a Feature before in the string format
        /// 
        /// </remarks>
        IApplicationStructureConfigurationBuilder Include(ApplicationArea area, string format);

        /// <summary>
        /// Build an <see cref="IApplicationStructureConfigurationBuilder"/>
        /// </summary>
        /// <returns>A built version of the <see cref="IApplicationStructureConfigurationBuilder"/></returns>
        IApplicationStructure Build();
    }
}