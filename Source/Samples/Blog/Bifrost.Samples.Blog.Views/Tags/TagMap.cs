using FluentNHibernate.Mapping;

namespace Bifrost.Samples.Blog.Views.Tags
{
    public class TagMap : ClassMap<Tag>
    {
        public TagMap()
        {
            Table("Tags");
            Id(t => t.Id).GeneratedBy.Assigned();
            Map(t => t.Name);
        }
    }
}