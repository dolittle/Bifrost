using System;
using Bifrost.Events;

namespace Bifrost.Fakes.Events.v3
{
    public class SimpleEvent : v2.SimpleEvent, IAmNextGenerationOf<v2.SimpleEvent>
    {
        public static string DEFAULT_VALUE_FOR_THIRD_GENERATION_PROPERTY = "3rd: DEFAULT";

        public string ThirdGenerationProperty { get; set; }

        public SimpleEvent(Guid eventSourceId) : this(eventSourceId,Guid.NewGuid())
        {}

        public SimpleEvent(Guid eventSourceId, Guid id) : base(eventSourceId,id)
        {
            ThirdGenerationProperty = DEFAULT_VALUE_FOR_THIRD_GENERATION_PROPERTY;
        }
    }
}