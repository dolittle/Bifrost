using System;
using Bifrost.Concepts;
using Bifrost.NHibernate.UserTypes;

namespace Bifrost.NHibernate.Concepts
{
    public class OracleGuidValueType<T> : ConceptValueType<T, Guid>
        where T : ConceptAs<Guid>
    {
        static OracleGuidMapping _oracleGuidMapping = new OracleGuidMapping();

        public OracleGuidValueType()
            : base(_oracleGuidMapping)
        {
            
        }
    }
}