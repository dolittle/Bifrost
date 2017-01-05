/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Bifrost.Validation.MetaData
{
    /// <summary>
    /// Represents the validation metadata for a type
    /// </summary>
    public class TypeMetaData
    {
        Dictionary<string, Dictionary<string, Rule>> _propertiesWithRules = new Dictionary<string, Dictionary<string, Rule>>();

        /// <summary>
        /// Gets the ruleset for a specific property
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public Dictionary<string, Rule>   this[string property]
        {
            get 
            {
                if (!_propertiesWithRules.ContainsKey(property))
                    _propertiesWithRules[property] = new Dictionary<string, Rule>();
                return _propertiesWithRules[property]; 
            }
        }


        /// <summary>
        /// Gets the properties with rulesets
        /// </summary>
        public Dictionary<string, Dictionary<string, Rule>> Properties { get { return _propertiesWithRules; } }
    }
}
