using System;
using Bifrost.Sagas;
using Bifrost.Samples.Blog.Events.Posts;

namespace Bifrost.Samples.Blog.Sagas.Posts
{
	public class Comments : Chapter
	{
		public string Author { get; set; }
		public string Comment { get; set; }
		public string EMail { get; set; }
		public string Url { get; set; }
		public DateTime Occured { get; set; }

		public void Process(CommentAdded @event)
		{
			Author = @event.Author;
			Comment = @event.Comment;
			EMail = @event.EMail;
			Url = @event.Url;
			Occured = @event.Occured;
		}
	}
}
