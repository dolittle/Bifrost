using Bifrost.Domain;
using Bifrost.Events;

namespace Bifrost.Specs.Domain.for_AggregateRootRepository
{
    public class AggregateRootWithEventSourceIdConstructor : AggregateRoot
    {
        public AggregateRootWithEventSourceIdConstructor(EventSourceId id) : base(id)
        {

        }
    }
}
