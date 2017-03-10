/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Applications
{
    /// <summary>
    /// Defines a system that is capable of converting between <see cref="ApplicationResourceIdentifier"/>
    /// and other representations, typically a <see cref="string"/>
    /// </summary>
    public interface IApplicationResourceIdentifierConverter
    {
        /// <summary>
        /// Get a string representation of the resource
        /// </summary>
        /// <param name="identifier"><see cref="ApplicationResourceIdentifier">Resource</see> to represent as string</param>
        /// <returns><see cref="String"/> representing the resource</returns>
        string AsString(ApplicationResourceIdentifier identifier);

        /// <summary>
        /// Translate a <see cref="string"/> to a <see cref="ApplicationResourceIdentifier"/>
        /// </summary>
        /// <param name="identifierAsString"><see cref="String"/> representing the resource</param>
        /// <returns><see cref="ApplicationResourceIdentifier">Identifier</see> for the resource</returns>
        ApplicationResourceIdentifier FromString(string identifierAsString);
    }
}
