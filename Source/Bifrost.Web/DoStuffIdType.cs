using System.Data;
using Bifrost.NHibernate.UserTypes;
using NHibernate;

namespace Bifrost.Web
{
    public class DoStuffIdType : ValueTypeOfString<DoStuffId>
    {
        public override object NullSafeGet(IDataReader rs, string[] names, object owner)
        {
            DoStuffId id = NHibernateUtil.String.NullSafeGet(rs, names[0]) as string;
            return id;
        }

        public override void NullSafeSet(IDbCommand cmd, object value, int index)
        {
            string id = value as DoStuffId;
            NHibernateUtil.String.NullSafeSet(cmd, id, index);
        }
    }
}