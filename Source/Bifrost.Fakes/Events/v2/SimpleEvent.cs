using System;
using Bifrost.Events;

namespace Bifrost.Fakes.Events.v2
{
    public class SimpleEvent : Events.SimpleEvent, IAmNextGenerationOf<Events.SimpleEvent>
    {
        public static string DEFAULT_VALUE_FOR_SECOND_GENERATION_PROPERTY = "2nd: DEFAULT";

        public string SecondGenerationProperty { get; set; }

        public SimpleEvent(Guid eventSourceId) : this(eventSourceId, Guid.NewGuid())
        {}

        public SimpleEvent(Guid eventSourceId, Guid id) : base(eventSourceId, id)
        {
            SecondGenerationProperty = DEFAULT_VALUE_FOR_SECOND_GENERATION_PROPERTY;
        }
    }
}