using System;
using Bifrost.Events;

namespace Bifrost.Samples.Blog.Events.Posts
{
	public class CommentAdded : Event
	{
		public CommentAdded(Guid postId)
			: base(postId)
		{
			
		}


		public CommentAdded(Guid postId, string author, string email, string url, string comment, DateTime occured)
			:base(postId)
		{
			Author = author;
			EMail = email;
			Url = url;
			Comment = comment;
			Occured = occured;
		}

		public string Author { get; set; }
		public string EMail { get; set; }
		public string Url { get; set; }
		public string Comment { get; set; }
		public DateTime Occured { get; set; }
	}
}
