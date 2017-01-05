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
using NHibernate.Type;

namespace Bifrost.NHibernate.UserTypes
{
    /// <summary>
    /// Represents a <see cref="NullSafeMapping">property mapping strategy</see> that uses the inbuilt type inference within NHibernate
    /// </summary>
    public class InferredMapping : NullSafeMapping
    {
#pragma warning disable 1591
        public override object Get(PropertyInfo property, IDataReader dr, string propertyName, ISessionImplementor session, object owner)
        {
            return GuessType(property.PropertyType).NullSafeGet(dr, propertyName, session, owner); ;
        }

        public override void Set(PropertyInfo property, object value, IDbCommand cmd, int index, ISessionImplementor session)
        {
            var propValue = property.GetValue(value, null);
            GuessType(property.PropertyType).NullSafeSet(cmd, propValue, index, session);
        }
#pragma warning restore 1591

        static IType GuessType(Type type)
        {
            var t = type.IsConcept() ? type.GetConceptValueType() : type;
            return NHibernateUtil.GuessType(t);
        }
    }
}