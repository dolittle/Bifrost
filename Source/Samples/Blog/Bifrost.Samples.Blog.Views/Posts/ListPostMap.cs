using FluentNHibernate.Mapping;

namespace Bifrost.Samples.Blog.Views.Posts
{
    public class ListPostMap : ClassMap<ListPost>
    {
        public ListPostMap()
        {
            Table("ListPosts");
            Id(p => p.Id);
            Map(p => p.Title);
        }
    }
}