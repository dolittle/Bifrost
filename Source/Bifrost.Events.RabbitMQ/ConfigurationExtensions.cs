/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Events
{
    /// <summary>
    /// Extensions for configuring RabbitMQ related communication for <see cref="IEvent">events</see>
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static CommittedEventStreamSenderConfiguration UsingRabbitMQ(this CommittedEventStreamSenderConfiguration configuration)
        {
            configuration.CommittedEventStreamSender = typeof(RabbitMQ.CommittedEventStreamSender);

            return configuration;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static CommittedEventStreamReceiverConfiguration UsingRabbitMQ(this CommittedEventStreamReceiverConfiguration configuration)
        {
            configuration.CommittedEventStreamReceiver = typeof(RabbitMQ.CommittedEventStreamReceiver);

            return configuration;
        }
    }
}
