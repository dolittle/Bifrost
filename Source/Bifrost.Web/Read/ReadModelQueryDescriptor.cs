/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Bifrost.Web.Read
{
    public class ReadModelQueryDescriptor
    {
        public ReadModelQueryDescriptor()
        {
            PropertyFilters = new Dictionary<string, object>();
        }

        public string ReadModel { get; set; }
        public string GeneratedFrom { get; set; }
        public Dictionary<string, object> PropertyFilters { get; private set; }
    }
}
