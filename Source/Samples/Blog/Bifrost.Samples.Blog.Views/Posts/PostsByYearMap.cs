using FluentNHibernate.Mapping;

namespace Bifrost.Samples.Blog.Views.Posts
{
    public class PostsByYearMap : ClassMap<PostsByYear>
    {
        public PostsByYearMap()
        {
            Id(p => p.BlogId).GeneratedBy.Assigned();
            Map(p => p.Year);
            Map(p => p.January);
            Map(p => p.February);
            Map(p => p.March);
            Map(p => p.April);
            Map(p => p.May);
            Map(p => p.June);
            Map(p => p.July);
            Map(p => p.August);
            Map(p => p.September);
            Map(p => p.October);
            Map(p => p.November);
            Map(p => p.December);
        }
    }
}