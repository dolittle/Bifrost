/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Applications;
using StackExchange.Redis;

namespace Bifrost.Events.Redis
{
    /// <summary>
    /// Represents an implementaion of <see cref="IEventSequenceNumbers"/>
    /// </summary>
    public class EventSequenceNumbers : IEventSequenceNumbers
    {
        const string EventSequenceNumberKey = "GlobalEventSequenceNumber";
        const string EventSequenceNumberForTypePrefix = "EventSequenceNumberFor";

        IApplicationResourceIdentifierConverter _applicationResourceIdentifierConverter;
        IDatabase _database;

        /// <summary>
        /// Initializes a new instance of <see cref="EventSequenceNumbers"/>
        /// </summary>
        /// <param name="configuration"><see cref="EventSequenceNumbersConfiguration">Configuration</see></param>
        /// <param name="applicationResourceIdentifierConverter">Converter for converting <see cref="IApplicationResourceIdentifier"/> "/></param>
        public EventSequenceNumbers(
            EventSequenceNumbersConfiguration configuration, 
            IApplicationResourceIdentifierConverter applicationResourceIdentifierConverter)
        {
            _applicationResourceIdentifierConverter = applicationResourceIdentifierConverter;

            var redis = ConnectionMultiplexer.Connect(string.Join(";", configuration.ConnectionStrings));
            _database = redis.GetDatabase();
        }


        /// <inheritdoc/>
        public EventSequenceNumber Next()
        {
            long sequenceNumber = _database.StringIncrement(EventSequenceNumberKey);
            return sequenceNumber;
        }

        /// <inheritdoc/>
        public EventSequenceNumber NextForType(IApplicationResourceIdentifier identifier)
        {
            var key = $"{EventSequenceNumberForTypePrefix}-{_applicationResourceIdentifierConverter.AsString(identifier)}";
            long sequenceNumber = _database.StringIncrement(key);
            return sequenceNumber;
        }
    }
}
