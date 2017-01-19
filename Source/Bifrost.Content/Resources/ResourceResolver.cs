/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Content.Resources
{
    /// <summary>
    /// Represents an implementation of <see cref="IResourceResolver"/>
    /// </summary>
    public class ResourceResolver :  IResourceResolver
    {
        /// <summary>
        /// Resolve a string resource based upon name
        /// </summary>
        /// <param name="name">Name of resource</param>
        /// <returns>A resolved resource string</returns>
        public string Resolve(string name)
        {
            return string.Empty;
        }
    }
}