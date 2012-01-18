using FluentNHibernate.Mapping;

namespace Bifrost.Samples.Blog.Views.Blogs
{
    public class BlogMap : ClassMap<Blog>
    {
        public BlogMap()
        {
            Table("Blogs");
            Id(b => b.Id).GeneratedBy.Assigned();
            Map(b => b.Name);
            Map(b => b.TagLine);
            Map(b => b.Owner);
        }
    }
}