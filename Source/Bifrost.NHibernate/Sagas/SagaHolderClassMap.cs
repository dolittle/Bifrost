/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Sagas;
using FluentNHibernate.Mapping;

namespace Bifrost.NHibernate.Sagas
{
    public class SagaHolderClassMap : ClassMap<SagaHolder>
    {
        public SagaHolderClassMap()
        {
            Table("Sagas");
            Id(s => s.Id).GeneratedBy.Assigned();
            Map(s => s.Key).Column("[Key]");
            Map(s => s.Partition);
            Map(s => s.Name);
            Map(s => s.Type);
            Map(s => s.SerializedSaga);
            Map(s => s.SerializedChapters);
            Map(s => s.UncommittedEvents);
        }
    }
}
