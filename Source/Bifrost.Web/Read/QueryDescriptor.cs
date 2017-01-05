/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Bifrost.Web.Read
{
    public class QueryDescriptor
    {
        public QueryDescriptor()
        {
            Parameters = new Dictionary<string, object>();
        }

        public string NameOfQuery { get; set; }
        public string GeneratedFrom { get; set; }
        public IDictionary<string, object> Parameters { get; private set; }
    }
}
