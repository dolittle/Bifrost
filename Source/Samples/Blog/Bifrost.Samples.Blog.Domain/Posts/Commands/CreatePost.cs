using System;
using Bifrost.Commands;

namespace Bifrost.Samples.Blog.Domain.Posts.Commands
{
    public class CreatePost : ICommand
	{
		public Guid Id { get; set; }
        public Guid BlogId { get; set; }
		public string Title { get; set; }
        public string Body { get; set; }
        public DateTime PublishDate { get; set; }

		public CreatePost()
		{
			Id = Guid.NewGuid();
		}
	}
}
