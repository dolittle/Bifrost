using System;
using Bifrost.Domain;

namespace Bifrost.Specs.Domain.for_AggregateRootRepository
{
    public class AggregateRootWithInvalidConstructorParameter : AggregateRoot
    {
        public AggregateRootWithInvalidConstructorParameter(int something) : base(Guid.NewGuid())
        {

        }
    }
}
