/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.IO;
using Bifrost.Applications;
using Bifrost.Execution;

namespace Bifrost.Events.Files
{
    /// <summary>
    /// Represents a simple and naïve implementation of <see cref="IEventSequenceNumbers"/>
    /// </summary>
    [Singleton]
    public class EventSequenceNumbers : IEventSequenceNumbers
    {
        EventSequenceNumbersConfiguration _configuration;
        object _globalSequenceLock = new object();
        Dictionary<int, object> _sequenceLocksPerType = new Dictionary<int, object>();
        IApplicationResourceIdentifierConverter _applicationResourceIdentifierConverter;

        /// <summary>
        /// Initializes a new instance of <see cref="EventSequenceNumbers"/>
        /// </summary>
        /// <param name="configuration"><see cref="EventSequenceNumbersConfiguration">Configuration</see>"/></param>
        /// <param name="applicationResourceIdentifierConverter"><see cref="IApplicationResourceIdentifierConverter"/> for getting string representation of <see cref="IApplicationResourceIdentifier"/></param>
        public EventSequenceNumbers(EventSequenceNumbersConfiguration configuration, IApplicationResourceIdentifierConverter applicationResourceIdentifierConverter)
        {
            _configuration = configuration;
            _applicationResourceIdentifierConverter = applicationResourceIdentifierConverter;
        }


        /// <inheritdoc/>
        public EventSequenceNumber Next()
        {
            lock( _globalSequenceLock )
            {
                MakeSurePathExists();

                var file = Path.Combine(_configuration.Path, "sequence");
                var sequence = GetNextInSequenceFromFile(file);
                File.WriteAllText(file, sequence.ToString());
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
                var file = Path.Combine(_configuration.Path, $"sequence_for_{identifierAsString}");

                var sequence = GetNextInSequenceFromFile(file);
                File.WriteAllText(file, sequence.ToString());
                return sequence;
            }
        }

        long GetNextInSequenceFromFile(string file)
        {
            var sequence = 0L;
            if (!File.Exists(file)) File.WriteAllText(file, sequence.ToString());

            using (var stream = new FileStream(file, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
            {
                using (var reader = new StreamReader(stream))
                {
                    sequence = long.Parse(reader.ReadToEnd().Trim());
                }
            }

            using (var stream = new FileStream(file, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
            { 
                sequence++;
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(sequence);
                }
            }

            return sequence;
        }

        void MakeSurePathExists()
        {
            if (!Directory.Exists(_configuration.Path)) Directory.CreateDirectory(_configuration.Path);
        }
    }
}
