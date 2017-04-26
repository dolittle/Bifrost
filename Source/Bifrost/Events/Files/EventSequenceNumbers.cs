/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Bifrost.Applications;
using Bifrost.Execution;
using Bifrost.Logging;

namespace Bifrost.Events.Files
{
    /// <summary>
    /// Represents a simple and naïve implementation of <see cref="IEventSequenceNumbers"/>
    /// </summary>
    [Singleton]
    public class EventSequenceNumbers : IEventSequenceNumbers
    {
        const string SequenceFileName = "sequence";
        const string SequenceForPrefix = "sequence_for_";

        EventSequenceNumbersConfiguration _configuration;
        object _globalSequenceLock = new object();
        Dictionary<int, object> _sequenceLocksPerType = new Dictionary<int, object>();
        IApplicationResourceIdentifierConverter _applicationResourceIdentifierConverter;
        IFiles _files;

        /// <summary>
        /// Initializes a new instance of <see cref="EventSequenceNumbers"/>
        /// </summary>
        /// <param name="configuration"><see cref="EventSequenceNumbersConfiguration">Configuration</see>"/></param>
        /// <param name="applicationResourceIdentifierConverter"><see cref="IApplicationResourceIdentifierConverter"/> for getting string representation of <see cref="IApplicationResourceIdentifier"/></param>
        /// <param name="files"><see cref="IFiles"/> to work with files</param>
        /// <param name="logger"><see cref="ILogger"/> for logging</param>
        public EventSequenceNumbers(
            EventSequenceNumbersConfiguration configuration, 
            IApplicationResourceIdentifierConverter applicationResourceIdentifierConverter, 
            IFiles files,
            ILogger logger)
        {
            _configuration = configuration;
            _applicationResourceIdentifierConverter = applicationResourceIdentifierConverter;
            _files = files;
            logger.Information($"Using path : {configuration.Path}");
        }


        /// <inheritdoc/>
        public EventSequenceNumber Next()
        {
            lock( _globalSequenceLock )
            {
                var sequence = GetNextInSequenceFromFile(SequenceFileName);
                _files.WriteString(_configuration.Path, SequenceFileName, sequence.ToString());
                return sequence;
            }
        }

        /// <inheritdoc/>
        public EventSequenceNumber NextForType(IApplicationResourceIdentifier identifier)
        {
            var hashCode = identifier.GetHashCode();
            lock( _sequenceLocksPerType )
            {
                if (!_sequenceLocksPerType.ContainsKey(hashCode)) _sequenceLocksPerType[hashCode] = new object();
            }

            lock( _sequenceLocksPerType[hashCode] )
            {
                var identifierAsString = _applicationResourceIdentifierConverter.AsString(identifier);
                var file = $"{SequenceForPrefix}{identifierAsString}";
                var sequence = GetNextInSequenceFromFile(file);
                _files.WriteString(_configuration.Path, file, sequence.ToString());
                return sequence;
            }
        }

        long GetNextInSequenceFromFile(string file)
        {
            var sequence = 0L;
            if( _files.Exists(_configuration.Path, file)) sequence = long.Parse(_files.ReadString(_configuration.Path, file));
            sequence++;
            _files.WriteString(_configuration.Path, file, sequence.ToString());
            return sequence;
        }
    }
}
