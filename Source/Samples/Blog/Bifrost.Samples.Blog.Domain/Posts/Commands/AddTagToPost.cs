using System;
using Bifrost.Commands;

namespace Bifrost.Samples.Blog.Domain.Posts.Commands
{
    public class AddTagToPost : ICommand
    {
        public Guid Id { get; set; }
        public string Tag { get; set; }
    }
}