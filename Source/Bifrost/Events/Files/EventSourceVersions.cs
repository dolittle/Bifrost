/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Applications;
using Bifrost.Logging;

namespace Bifrost.Events.Files
{
    /// <summary>
    /// Represents an implementation of <see cref="IEventSourceVersions"/> for holding <see cref="EventSourceVersion"/>
    /// for each <see cref="EventSource"/> on the filesystem
    /// </summary>
    public class EventSourceVersions : IEventSourceVersions
    {
        const string VersionForPrefix = "VersionFor";

        IFiles _files;
        IApplicationResourceIdentifierConverter _applicationResourceIdentifierConverter;
        IEventStore _eventStore;
        string _path;

        /// <summary>
        /// Initializes a new instance of <see cref="EventSourceVersions"/>
        /// </summary>
        /// <param name="files">A system to work with <see cref="IFiles"/></param>
        /// <param name="eventStore"><see cref="IEventStore"/> for getting information if not found in file system</param>
        /// <param name="applicationResourceIdentifierConverter">Converter for converting <see cref="IApplicationResourceIdentifier"/> "/></param>
        /// <param name="pathProvider">A delegate that can provide path to store <see cref="EventSourceVersion"/> for <see cref="IEventSource"/> - see <see cref="ICanProvideEventSourceVersionsPath"/></param>
        /// <param name="logger"><see cref="ILogger"/> for logging</param>
        public EventSourceVersions(
            IFiles files, 
            IEventStore eventStore, 
            IApplicationResourceIdentifierConverter applicationResourceIdentifierConverter, 
            ICanProvideEventSourceVersionsPath pathProvider,
            ILogger logger)
        {
            _files = files;
            _eventStore = eventStore;
            _applicationResourceIdentifierConverter = applicationResourceIdentifierConverter;
            _path = pathProvider();
            logger.Information($"Using path : {_path}");
        }

        /// <inheritdoc/>
        public EventSourceVersion GetFor(IApplicationResourceIdentifier eventSource, EventSourceId eventSourceId)
        {
            var fileName = GetFileNameFor(eventSource, eventSourceId);
            var version = EventSourceVersion.Zero;

            if( _files.Exists(_path, fileName) )
            {
                var versionAsString = _files.ReadString(_path, fileName);
                version = EventSourceVersion.FromCombined(double.Parse(versionAsString));
            }  else version = _eventStore.GetVersionFor(eventSource, eventSourceId);

            return version;
        }

        /// <inheritdoc/>
        public void SetFor(IApplicationResourceIdentifier eventSource, EventSourceId eventSourceId, EventSourceVersion version)
        {
            var fileName = GetFileNameFor(eventSource, eventSourceId);
            _files.WriteString(_path, fileName, version.Combine().ToString());
        }


        string GetFileNameFor(IApplicationResourceIdentifier eventSource, EventSourceId eventSourceId)
        {
            var key = $"{VersionForPrefix}_{_applicationResourceIdentifierConverter.AsString(eventSource)}_{eventSourceId.Value}";
            return key;
        }
    }
}
