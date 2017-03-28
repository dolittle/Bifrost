/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Applications
{
    /// <summary>
    /// Defines a system that is capable of converting between <see cref="IApplicationResourceIdentifier"/>
    /// and other representations, typically a <see cref="string"/>
    /// </summary>
    public interface IApplicationResourceIdentifierConverter
    {
        /// <summary>
        /// Get a string representation of the resource
        /// </summary>
        /// <param name="identifier"><see cref="IApplicationResourceIdentifier">Resource</see> to represent as string</param>
        /// <returns><see cref="string"/> representing the resource</returns>
        string AsString(IApplicationResourceIdentifier identifier);

        /// <summary>
        /// Translate a <see cref="string"/> to a <see cref="IApplicationResourceIdentifier"/>
        /// </summary>
        /// <param name="identifierAsString"><see cref="string"/> representing the resource</param>
        /// <returns><see cref="IApplicationResourceIdentifier">Identifier</see> for the resource</returns>
        IApplicationResourceIdentifier FromString(string identifierAsString);
    }
}
