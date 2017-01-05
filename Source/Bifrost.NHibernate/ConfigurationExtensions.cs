/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.NHibernate;
using Bifrost.NHibernate.Entities;
using Bifrost.NHibernate.Read;
using NHibernate;

namespace Bifrost.Configuration
{
    public static class ConfigurationExtensions
    {
        public static IConfigure UsingNHibernate(this IHaveStorage storage, Action<EntityContextConnection> connectionConfiguration)
        {
            var connection = new EntityContextConnection();
            connectionConfiguration(connection);
            storage.EntityContextConfiguration = new EntityContextConfiguration { Connection = connection };
            Configure.Instance.Container.Bind<IConnection>(new ReadConnection(() => connection.SessionFactory));
            return Configure.Instance;
        }

        public static IConfigure UsingNHibernate(this IHaveStorage storage, EntityContextConfiguration entityContextConfiguration, Func<ISessionFactory> getSessionFactory)
        {
            storage.EntityContextConfiguration = entityContextConfiguration;
            Configure.Instance.Container.Bind<IConnection>(new ReadConnection(getSessionFactory));
            return Configure.Instance;
        }
    }
}
