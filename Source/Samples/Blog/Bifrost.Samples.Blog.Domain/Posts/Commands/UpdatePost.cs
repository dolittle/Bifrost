using System;
using Bifrost.Commands;

namespace Bifrost.Samples.Blog.Domain.Posts.Commands
{
    public class UpdatePost : ICommand
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
