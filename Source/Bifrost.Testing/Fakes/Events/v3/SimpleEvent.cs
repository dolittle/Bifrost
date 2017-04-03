using System;
using Bifrost.Events;

namespace Bifrost.Testing.Fakes.Events.v3
{
    public class SimpleEvent : Testing.Fakes.Events.v2.SimpleEvent, IAmNextGenerationOf<Testing.Fakes.Events.v2.SimpleEvent>
    {
        public static string DEFAULT_VALUE_FOR_THIRD_GENERATION_PROPERTY = "3rd: DEFAULT";

        public string ThirdGenerationProperty { get; set; }

        public SimpleEvent(EventSourceId eventSourceId) : base(eventSourceId)
        {
            ThirdGenerationProperty = DEFAULT_VALUE_FOR_THIRD_GENERATION_PROPERTY;
        }
    }
}