using System;
using Bifrost.Views;

namespace Bifrost.Specs.Views
{
    public class SimpleObjectWithId : IHaveId
    {
        public SimpleObjectWithId()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
    }

    public class SimpleObjectWithoutId
    {
        
    }
}
