/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Bifrost.Diagnostics;
using Bifrost.Read;

namespace Bifrost.Web.Visualizer.QualityAssurance
{
    public class Problems : IReadModel
    {
        public IEnumerable<Problem> All { get; set; }
    }
}
