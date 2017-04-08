/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Configuration;
using Bifrost.Events.RabbitMQ;

namespace Bifrost.Events
{
    /// <summary>
    /// Extensions for configuring RabbitMQ related communication for <see cref="IEvent">events</see>
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Configure <see cref="ICanSendCommittedEventStream"/> using RabbitMQ
        /// </summary>
        /// <param name="configuration"><see cref="CommittedEventStreamReceiverConfiguration"/> to configure</param>
        /// <param name="connectionString">ConnectionString to connect with</param>
        /// <returns>Chained <see cref="CommittedEventStreamReceiverConfiguration"/></returns>
        public static CommittedEventStreamSenderConfiguration UsingRabbitMQ(this CommittedEventStreamSenderConfiguration configuration, string connectionString)
        {
            configuration.CommittedEventStreamSender = typeof(RabbitMQ.CommittedEventStreamSender);
            Configure.Instance.Container.Bind<ICanProvideConnectionStringToSender>(() => connectionString);
            return configuration;
        }


        /// <summary>
        /// Configure <see cref="ICanReceiveCommittedEventStream"/> using RabbitMQ
        /// </summary>
        /// <param name="configuration"><see cref="CommittedEventStreamReceiverConfiguration"/> to configure</param>
        /// <param name="connectionString">ConnectionString to connect with</param>
        /// <returns>Chained <see cref="CommittedEventStreamReceiverConfiguration"/></returns>
        public static CommittedEventStreamReceiverConfiguration UsingRabbitMQ(this CommittedEventStreamReceiverConfiguration configuration, string connectionString)
        {
            configuration.CommittedEventStreamReceiver = typeof(RabbitMQ.CommittedEventStreamReceiver);
            Configure.Instance.Container.Bind<ICanProvideConnectionStringToReceiver>(() => connectionString);
            return configuration;
        }
    }
}
