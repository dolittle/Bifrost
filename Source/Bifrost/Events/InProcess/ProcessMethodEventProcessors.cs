/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Bifrost.Applications;
using Bifrost.Execution;
using Bifrost.Time;

namespace Bifrost.Events.InProcess
{
    /// <summary>
    /// Represents an implementation of <see cref="IKnowAboutEventProcessors"/> for 
    /// <see cref="IEventProcessor">event processors</see> in the currently running process.
    /// </summary>
    /// <remarks>
    /// The <see cref="IEventProcessor">processors</see> this implementation deals with is your
    /// .NET based and discovered <see cref="IEventProcessor">processors</see>
    /// </remarks>
    [Singleton]
    public class ProcessMethodEventProcessors : IKnowAboutEventProcessors
    {
        /// <summary>
        /// Name of method that any event subscriber needs to be called in order to be recognized by the convention
        /// </summary>
        public const string ProcessMethodName = "Process";

        List<IEventProcessor> _eventProcessors = new List<IEventProcessor>();
        
        ITypeDiscoverer _typeDiscoverer;
        IContainer _container;
        ISystemClock _systemClock;

        /// <summary>
        /// Initializes a new instance of <see cref="ProcessMethodEventProcessors"/>
        /// </summary>
        /// <param name="applicationResources"><see cref="IApplicationResources"/> for identifying <see cref="IEvent">events</see> </param>
        /// <param name="typeDiscoverer"><see cref="ITypeDiscoverer"/> for discovering implementations of <see cref="IProcessEvents"/></param>
        /// <param name="container"><see cref="IContainer"/> for the implementation <see cref="ProcessMethodEventProcessor"/> when acquiring instances of implementations of <see cref="IProcessEvents"/></param>
        /// <param name="systemClock"><see cref="ISystemClock"/> for timing <see cref="IEventProcessors"/></param>
        public ProcessMethodEventProcessors(IApplicationResources applicationResources, ITypeDiscoverer typeDiscoverer, IContainer container, ISystemClock systemClock)
        {
            _typeDiscoverer = typeDiscoverer;
            _container = container;
            _systemClock = systemClock;

            PopulateEventProcessors(applicationResources);
        }

        /// <inheritdoc/>
        public IEnumerator<IEventProcessor> GetEnumerator()
        {
            return _eventProcessors.GetEnumerator();
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _eventProcessors.GetEnumerator();
        }

        void PopulateEventProcessors(IApplicationResources applicationResources)
        {
            var processors = _typeDiscoverer.FindMultiple<IProcessEvents>();
            foreach (var processor in processors)
            {
                var methods = processor.GetTypeInfo().GetMethods(BindingFlags.Public | BindingFlags.Instance).Where(m =>
                {
                    var parameters = m.GetParameters();
                    return
                        m.Name.Equals(ProcessMethodName) &&
                        parameters.Length == 1 &&
                        typeof(IEvent).GetTypeInfo().IsAssignableFrom(parameters[0].ParameterType.GetTypeInfo());
                });

                foreach (var method in methods)
                {
                    var eventIdentifier = applicationResources.Identify(method.GetParameters()[0].ParameterType);
                    var processMethodEventProcessor = new ProcessMethodEventProcessor(_container, _systemClock, "", eventIdentifier, method);
                    _eventProcessors.Add(processMethodEventProcessor);
                }
            }
        }
    }
}
