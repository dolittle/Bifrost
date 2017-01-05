/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using NHibernate;

namespace Bifrost.NHibernate
{
    /// <summary>
    /// Represents an NHibernate connection.  Provides access to the configured SessionFactory
    /// </summary>
    public interface IConnection
    {
        /// <summary>
        /// Gets an instance of ISessionFactory
        /// </summary>
       ISessionFactory SessionFactory { get; } 
    }
}