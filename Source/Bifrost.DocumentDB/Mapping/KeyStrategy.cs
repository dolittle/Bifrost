/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Mapping;

namespace Bifrost.DocumentDB.Mapping
{
    /// <summary>
    /// Represents an implementation of <see cref="IPropertyMappingStrategy"/> for defining a key
    /// </summary>
    public class KeyStrategy : IPropertyMappingStrategy
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mappingTarget"></param>
        /// <param name="target"></param>
        /// <param name="value"></param>
        public void Perform(IMappingTarget mappingTarget, object target, object value)
        {
            throw new NotImplementedException();
        }
    }
}
