/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Dynamic;

namespace Bifrost.Entities.Files
{
    /// <summary>
    /// Represents an object that acts as the "document" that gets saved to disk
    /// </summary>
    public class Document : DynamicObject
    {
        Dictionary<string, object> _hash = new Dictionary<string, object>();
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="binder"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            _hash[binder.Name] = value;
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="binder"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (_hash.ContainsKey(binder.Name))
            {
                result = _hash[binder.Name];
                return true;
            }
            result = null;
            return false;
        }
    }
}
