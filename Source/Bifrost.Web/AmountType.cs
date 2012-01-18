using System.Data;
using Bifrost.NHibernate.UserTypes;
using NHibernate;

namespace Bifrost.Web
{
    public class AmountType : ValueTypeOfDecimal<Amount>
    {
        public override object NullSafeGet(IDataReader rs, string[] names, object owner)
        {
            Amount amount = (decimal)NHibernateUtil.Decimal.NullSafeGet(rs, names[0]);
            return amount;
        }

        public override void NullSafeSet(IDbCommand cmd, object value, int index)
        {
            decimal amount = value as Amount;
            NHibernateUtil.Decimal.NullSafeSet(cmd, amount, index);
        }
    }
}