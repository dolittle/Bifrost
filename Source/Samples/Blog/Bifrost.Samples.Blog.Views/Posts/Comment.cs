using System;
using Bifrost.Views;

namespace Bifrost.Samples.Blog.Views.Posts
{
	public class Comment : IHaveId
	{
		public Guid Id { get; set; }

		public Guid PostId { get; set; }

		public string Author { get; set; }
		public string EMail { get; set; }
		public string Url { get; set; }
		public string Body { get; set; }
		public DateTime Occured { get; set; }
	}
}