using FluentNHibernate.Mapping;

namespace Bifrost.Web
{
    public class DoStuffClassMap : ClassMap<DoStuff>
    {
        public DoStuffClassMap()
        {
            Table("DoStuff");
            Id(x => x.Id).CustomType<DoStuffIdType>().GeneratedBy.Assigned();
            Map(x => x.AnotherValue);
            Map(x => x.Amount).CustomType<AmountType>();
            Map(x => x.Timestamp).CustomType<TimestampType>();
        }
    }
}