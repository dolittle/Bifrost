using System;
using Bifrost.Events;

namespace Bifrost.Fakes.Events.v2
{
    public class SimpleEvent : Events.SimpleEvent, IAmNextGenerationOf<Events.SimpleEvent>
    {
        public static string DEFAULT_VALUE_FOR_SECOND_GENERATION_PROPERTY = "2nd: DEFAULT";

        public string SecondGenerationProperty { get; set; }

        public SimpleEvent(Guid eventSourceId) : this(eventSourceId, 0)
        {}

        public SimpleEvent(Guid eventSourceId, long id) : base(eventSourceId, id)
        {
            SecondGenerationProperty = DEFAULT_VALUE_FOR_SECOND_GENERATION_PROPERTY;
        }
    }
}