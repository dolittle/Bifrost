using Bifrost.Views;
using System;

namespace Bifrost.Web
{
    public class StuffToPersist : IHaveId
    {
        public Guid Id { get; set; }
        public string Something { get; set; }
    }
}