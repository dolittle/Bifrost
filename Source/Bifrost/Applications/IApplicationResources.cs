/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Applications
{
    /// <summary>
    /// Defines a utility for working with application resources
    /// </summary>
    public interface IApplicationResources
    {
        /// <summary>
        /// Identify a resource
        /// </summary>
        /// <param name="resource">Resource to identify</param>
        /// <returns><see cref="ApplicationResourceIdentifier"/> identifying the resource</returns>
        ApplicationResourceIdentifier Identify(object resource);

        /// <summary>
        /// Get a string representation of the resource
        /// </summary>
        /// <param name="resource"><see cref="ApplicationResourceIdentifier">Resource</see> to represent as string</param>
        /// <returns><see cref="String"/> representing the resource</returns>
        string AsString(ApplicationResourceIdentifier resource);

        /// <summary>
        /// Translate a <see cref="string"/> to a <see cref="ApplicationResourceIdentifier"/>
        /// </summary>
        /// <param name="resource"><see cref="String"/> representing the resource</param>
        /// <returns><see cref="ApplicationResourceIdentifier">Identifier</see> for the resource</returns>
        ApplicationResourceIdentifier FromString(string resource);
    }
}
