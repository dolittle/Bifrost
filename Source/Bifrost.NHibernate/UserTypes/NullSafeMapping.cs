/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Data;
using System.Reflection;
using NHibernate.Engine;

namespace Bifrost.NHibernate.UserTypes
{
    /// <summary>
    /// Defines a strategy for mapping properties to commands and from data readers with NHibernate
    /// </summary>
    public abstract class NullSafeMapping
    {
        /// <summary>
        /// Retrieves the value of a custom type from a data reader
        /// </summary>
        /// <param name="property">Property Info representing the mapped property</param>
        /// <param name="dr">Date Reader</param>
        /// <param name="propertyName">Name of the property being mapped</param>
        /// <param name="session">NHibernate Session</param>
        /// <param name="owner">Owner object/param>
        /// <returns></returns>
        public abstract object Get(PropertyInfo property, IDataReader dr, string propertyName, ISessionImplementor session, object owner);
        /// <summary>
        /// Sets the value of a custom type in to an IDbCommand
        /// </summary>
        /// <param name="property">Property Info representing the mapped property</param>
        /// <param name="value">The value to set into the Property</param>
        /// <param name="cmd">Database Command</param>
        /// <param name="index">Index position of the property</param>
        /// <param name="session">NHibernate Session</param>
        public abstract void Set(PropertyInfo property, object value, IDbCommand cmd, int index, ISessionImplementor session);
    }
}