using System;
using Bifrost.Domain;

namespace Bifrost.Fakes.Domain
{
    public class StatelessAggregatedRoot : AggregatedRoot
    {
        public StatelessAggregatedRoot(Guid id) : base(id) {}
    }
}