using System;
using Bifrost.Views;

namespace Bifrost.Samples.Blog.Views.Tags
{
    public class Tag : IHaveId
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
