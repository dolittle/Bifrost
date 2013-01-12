using Bifrost.Events;
using Bifrost.MSpec.Events;

namespace Bifrost.MSpec.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="UncommittedEventStream"/> to help with testing / specifications.
    /// </summary>
    public static class UncommittedEventStreamExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">The type of the <see cref="Event"/> to assert should be present</typeparam>
        /// <param name="uncommittedEventStream">The <see cref="UncommittedEventStream"/> to search.</param>
        /// <returns>An <see cref="EventSequenceValidation"/> of the EventType which can be asserted against.></returns>
        public static EventSequenceValidation<T> ShouldHaveEvent<T>(this UncommittedEventStream uncommittedEventStream) where T : IEvent
        {
            var sequenceValidation = new EventSequenceValidation<T>(uncommittedEventStream);
            return sequenceValidation;
        }
    }
}