/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Configuration
{
    /// <summary>
    /// Represents an interface for a system that gets invoked when configuration has been done
    /// </summary>
    public interface IWantToKnowWhenConfigurationIsDone
    {
        /// <summary>
        /// Method that gets called when <see cref="IConfigure"/> is completed
        /// </summary>
        /// <param name="configure"><see cref="IConfigure"/> object that was configured</param>
        void Configured(IConfigure configure);
    }
}
