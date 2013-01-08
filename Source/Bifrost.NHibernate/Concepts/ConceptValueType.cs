using System;
using Bifrost.Concepts;
using Bifrost.NHibernate.UserTypes;


namespace Bifrost.NHibernate.Concepts
{
    public class ConceptValueType<T,U> : ImmutableUserType<T> where U : IEquatable<U> where T : ConceptAs<U>
    {
        public ConceptValueType()
        {
            MapProperty(concept => concept.Value);
        }
    }
}
