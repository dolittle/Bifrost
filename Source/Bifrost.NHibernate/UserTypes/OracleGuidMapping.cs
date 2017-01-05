/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Data;
using System.Reflection;
using Bifrost.Concepts;
using NHibernate;
using NHibernate.Engine;

namespace Bifrost.NHibernate.UserTypes
{
    /// <summary>
    /// Represents a <see cref="NullSafeMapping">property mapping strategy</see> for handing Guids to and from an Oracle database
    /// </summary>
    public class OracleGuidMapping : NullSafeMapping
    {
#pragma warning disable 1591
        public override object Get(PropertyInfo property, IDataReader dr, string propertyName, ISessionImplementor session, object owner)
        {
            var buffer = (byte[])NHibernateUtil.Binary.NullSafeGet(dr, propertyName, session, owner);
            if (null != buffer)
            {
                var result = new Guid(buffer);
                return result;
            }
            return Guid.Empty;
        }

        public override void Set(PropertyInfo property, object value, IDbCommand cmd, int index, ISessionImplementor session)
        {
            if (value == null)
                return;

            var guidValue = Guid.Empty;

            if (value is Guid)
                guidValue = (Guid) value;

            var guidAsConcept = value as ConceptAs<Guid>;
            if (guidAsConcept != null)
                guidValue = guidAsConcept.Value;

            if(guidValue == Guid.Empty)
                throw new InvalidOperationException("Invalid type: " + value.GetType());

            NHibernateUtil.Binary.NullSafeSet(cmd, guidValue.ToByteArray(), index);
        }
#pragma warning restore 1591
    }
}