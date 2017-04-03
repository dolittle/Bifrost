using System;
using Bifrost.Events;

namespace Bifrost.Testing.Fakes.Events.v2
{
    public class SimpleEvent : Events.SimpleEvent, IAmNextGenerationOf<Events.SimpleEvent>
    {
        public static string DEFAULT_VALUE_FOR_SECOND_GENERATION_PROPERTY = "2nd: DEFAULT";

        public string SecondGenerationProperty { get; set; }


        public SimpleEvent(EventSourceId  eventSourceId) : base(eventSourceId)
        {
            SecondGenerationProperty = DEFAULT_VALUE_FOR_SECOND_GENERATION_PROPERTY;
        }
    }
}