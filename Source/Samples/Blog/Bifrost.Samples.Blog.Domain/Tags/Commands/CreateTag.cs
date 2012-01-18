using System;
using Bifrost.Commands;

namespace Bifrost.Samples.Blog.Domain.Tags.Commands
{
    public class CreateTag : ICommand
    {
        public CreateTag()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string TagName { get; set; }
    }
}
