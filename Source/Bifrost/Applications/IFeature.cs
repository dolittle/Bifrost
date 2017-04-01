/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Applications
{
    /// <summary>
    /// Defines a <see cref="IFeature"/> of a system
    /// </summary>
    public interface IFeature : IApplicationLocation<FeatureName>, IBelongToAnApplicationLocationTypeOf<IModule>, ICanHaveApplicationLocationsOfType<ISubFeature>
    {
        /// <summary>
        /// Add a <see cref="SubFeature"/> 
        /// </summary>
        /// <param name="subFeature"><see cref="SubFeature"/> to add</param>
        void AddSubFeature(ISubFeature subFeature);
    }
}