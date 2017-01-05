/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.CodeGeneration.JavaScript
{
    /// <summary>
    /// Provides methods for working with <see cref="TypeExtension"/>
    /// </summary>
    public static class TypeExtensionExtensions
    {
        /// <summary>
        /// Specifiy the super type for the <see cref="TypeExtension"/>
        /// </summary>
        /// <param name="typeExtension"><see cref="TypeExtension"/> to set the super for</param>
        /// <param name="super">Super to set</param>
        /// <returns>Chained <see cref="TypeExtension"/> to keep building on</returns>
        public static TypeExtension WithSuper(this TypeExtension typeExtension, string super)
        {
            typeExtension.SuperType = super;
            return typeExtension;
        }
    }
}
