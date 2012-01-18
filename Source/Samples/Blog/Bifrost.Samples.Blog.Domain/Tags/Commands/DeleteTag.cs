using System;
using Bifrost.Commands;

namespace Bifrost.Samples.Blog.Domain.Tags.Commands
{
    public class DeleteTag : ICommand
    {
        public Guid Id { get; set; }
    }
}