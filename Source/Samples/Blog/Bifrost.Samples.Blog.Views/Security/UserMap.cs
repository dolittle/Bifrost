using FluentNHibernate.Mapping;

namespace Bifrost.Samples.Blog.Views.Security
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table("Users");
            Id(u => u.Id).GeneratedBy.Assigned();
            Map(u => u.UserName);
            Map(u => u.Password);
        }
    }
}