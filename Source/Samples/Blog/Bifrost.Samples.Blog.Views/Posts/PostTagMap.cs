using FluentNHibernate.Mapping;

namespace Bifrost.Samples.Blog.Views.Posts
{
    public class PostTagMap : ClassMap<PostTag>
    {
        public PostTagMap()
        {
            Table("PostTags");
            Id(p => p.Id).GeneratedBy.Assigned();
            Map(p => p.Name);
        }
    }
}