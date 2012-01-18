using FluentNHibernate.Mapping;

namespace Bifrost.Samples.Blog.Views.Posts
{
	public class CommentMap : ClassMap<Comment>
	{
		public CommentMap()
		{
			Table("Comments");
			Id(c => c.Id).GeneratedBy.Assigned();
			Map(c => c.PostId);
			Map(c => c.Author);
			Map(c => c.EMail);
			Map(c => c.Url);
			Map(c => c.Body);
			Map(c => c.Occured);
		}
	}
}