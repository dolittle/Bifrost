using System;
using Bifrost.Domain;

namespace Bifrost.Web
{
    public class TheObject : AggregatedRoot
    {
        public TheObject(Guid id)
            : base(id)
        {
        }


        public void DoStuff(string something)
        {
            Apply(new StuffDone(Id) { Something = something });
        }


        void Handle(StuffDone @event)
        {
        }
    }
}