using FluentNHibernate.Mapping;

namespace Bifrost.Samples.Blog.Views.Posts
{
	public class PostMap : ClassMap<Post>
	{
		public PostMap()
		{
			Table("Posts");
			Id(p => p.Id).GeneratedBy.Assigned();
			Map(p => p.Title);
		    Map(p => p.Body);
		    Map(p => p.Author);
		    Map(p => p.UpdateDate);
		    Map(p => p.PublishDate);
		    Map(p => p.BlogId);
		    Map(p => p.MonthOfPublication);
		    Map(p => p.YearOfPublication);
		}
	}
}