/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Content.Resources
{
    /// <summary>
    /// Defines a properties resvoler that can resolve any instance properties 
    /// </summary>
    public interface IResourcePropertiesResolver
    {
        /// <summary>
        /// Resolve all properties for a specific type
        /// </summary>
        /// <typeparam name="T">Type to resolve for - can be implicit from the instance</typeparam>
        /// <param name="instance">Instance to resolve</param>
        void ResolvePropertiesFor<T>(T instance);
    }
}