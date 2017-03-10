using System;
using Bifrost.Domain;

namespace Bifrost.Specs.Domain.for_AggregateRootRepository
{
    public class AggregateRootWithMultipleConstructorParameters : AggregateRoot
    {
        public AggregateRootWithMultipleConstructorParameters(Guid id, int something) : base(id)
        {

        }
    }
}
