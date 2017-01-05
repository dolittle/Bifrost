/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Security
{
    /// <summary>
    /// Defines the builder for building a <see cref="ISecurityDescriptor"/>
    /// </summary>
    public interface ISecurityDescriptorBuilder
    {
        /// <summary>
        /// Gets the <see cref="ISecurityDescriptor"/> that is used by the builder
        /// </summary>
        ISecurityDescriptor Descriptor { get; }
    }
}
