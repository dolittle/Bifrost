/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Configuration;
using Bifrost.Entities;

namespace Bifrost.NHibernate.Entities
{
    public class EntityContextConfiguration : IEntityContextConfiguration
    {
        public Type EntityContextType { get { return typeof (EntityContext<>); } }
        public IEntityContextConnection Connection { get; set; }
    }
}
