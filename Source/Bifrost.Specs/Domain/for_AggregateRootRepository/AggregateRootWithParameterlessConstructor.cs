using System;
using Bifrost.Domain;

namespace Bifrost.Specs.Domain.for_AggregateRootRepository
{
    public class AggregateRootWithParameterlessConstructor : AggregateRoot
    {
        public AggregateRootWithParameterlessConstructor() : base(Guid.NewGuid())
        {

        }
    }
}
