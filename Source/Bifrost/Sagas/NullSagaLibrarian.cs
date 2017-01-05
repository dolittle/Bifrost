/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;

namespace Bifrost.Sagas
{
    /// <summary>
    /// Represents a null implementation of <see cref="ISagaLibrarian"/>
    /// </summary>
    public class NullSagaLibrarian : ISagaLibrarian
    {
#pragma warning disable 1591 // Xml Comments
        public void Close(ISaga saga)
        {
        }

        public void Catalogue(ISaga saga)
        {
        }

        public ISaga Get(Guid id)
        {
            return null;
        }

        public ISaga Get(string partition, string key)
        {
            return null;
        }

        public IEnumerable<ISaga> GetForPartition(string partition)
        {
            return new ISaga[0];
        }
#pragma warning restore 1591 // Xml Comments
    }
}
