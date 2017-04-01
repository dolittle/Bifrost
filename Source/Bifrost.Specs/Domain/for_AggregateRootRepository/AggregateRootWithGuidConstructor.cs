using System;
using Bifrost.Domain;

namespace Bifrost.Specs.Domain.for_AggregateRootRepository
{
    public class AggregateRootWithGuidConstructor : AggregateRoot
    {
        public AggregateRootWithGuidConstructor(Guid id) : base(id)
        {

        }
    }
}
