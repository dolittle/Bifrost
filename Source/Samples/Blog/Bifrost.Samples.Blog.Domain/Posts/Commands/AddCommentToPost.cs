using System;
using Bifrost.Commands;

namespace Bifrost.Samples.Blog.Domain.Posts.Commands
{
	public class AddCommentToPost : Command
	{
		public string Author { get; set; }
		public string EMail { get; set; }
		public string Url { get; set; }
		public string Comment { get; set; }
		public DateTime Occured { get; set; }
	}
}
