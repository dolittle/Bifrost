/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Type;

namespace Bifrost.NHibernate.Read
{
    public class UTCDateTimesInterceptor : EmptyInterceptor
    {
        public override bool OnLoad(object entity, object id, object[] state,string[] propertyNames, IType[] types)
        {
            ConvertDatabaseDateTimeToUtc(state, types);
            return true;
        }

        private void ConvertDatabaseDateTimeToUtc(object[] state, IList<IType> types)
        {
            for (int i = 0; i < types.Count; i++)
            {
                if (!IsDateTime(types[i].ReturnedClass))
                    continue;

                var dateTime = state[i] as DateTime?;

                if (!dateTime.HasValue)
                    continue;

                if (dateTime.Value.Kind != DateTimeKind.Unspecified)
                    continue;

                state[i] = DateTime.SpecifyKind(dateTime.Value, DateTimeKind.Utc);
            }
        }

        bool IsDateTime(Type type)
        {
            return type == typeof (DateTime) || type == typeof (DateTime?);
        }
    }
}